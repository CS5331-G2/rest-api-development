using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;

public class ProxyHandler
{
    private HttpContext _httpContext;
    public HttpStatusCode _httpResponseStatus;
    private System.Net.HttpWebResponse _httpResponse;
    private string _response;

    public ProxyHandler(HttpContext httpContext) 
    {
        _httpContext = httpContext;
    }

    /// <see>http://stackoverflow.com/questions/3447589/copying-http-request-inputstream</see>
    public void ProxyToApi()
    {
        // Create a request for the URL.
        string apiEndpoint = BuildNewApiUri();
        HttpWebRequest request = WebRequest.CreateHttp(apiEndpoint);
        request.Method = _httpContext.Request.Method;

        //-- No need to copy input stream for GET (actually it would throw an exception)
        if (request.Method != "GET")
        {
            request.ContentType = "application/json";

            _httpContext.Request.EnableRewind();
            _httpContext.Request.Body.Position = 0;  //***** THIS IS REALLY IMPORTANT GOTCHA
                
            var requestStream = _httpContext.Request.Body;
            Stream webStream = null;
            try
            {
                //copy incoming request body to outgoing request
                if (requestStream != null && requestStream.Length > 0)
                {
                    request.ContentLength = requestStream.Length;
                    webStream = request.GetRequestStream();
                    requestStream.CopyTo(webStream);
                }
            }
            finally
            {
                if (null != webStream)
                {
                    webStream.Flush();
                    webStream.Close(); 
                }
            }
        }

        // If required by the server, set the credentials.
        //request.Credentials = CredentialCache.DefaultCredentials

        try 
        {
            _httpResponse = (HttpWebResponse)request.GetResponse();
            
            // Get the stream containing content returned by the server.
            using (Stream dataStream = _httpResponse.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content. 
                _response = reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            _httpResponseStatus = (ex.Response as HttpWebResponse).StatusCode;
            Debug.WriteLine(ex.Message, "error");
        }
    }

    public void WriteResponse(HttpResponse response) 
    {
        if (_httpResponse == null) 
        {
            response.StatusCode = (int)_httpResponseStatus;
            return;
        }

        for (int i = 0; i < _httpResponse.Headers.Count; i++) 
        {
            string key = _httpResponse.Headers.GetKey(i);
            if (key == "Content-Type")
            {
                response.Headers.Add(key, new StringValues(_httpResponse.Headers.GetValues(i)));
            }
        }
        response.StatusCode = (int)_httpResponse.StatusCode;
        
        using (StreamWriter sw = new StreamWriter(response.Body))
        {
            sw.Write(_response);   
        }
    }

    private string BuildNewApiUri()
    {
        string scheme = _httpContext.Request.Scheme;
        string host = _httpContext.Request.Host.Value;
        string path = _httpContext.Request.Path;

        if (host.Contains(diary.Program.WEBAPI_PORT)) 
        { // proxy to /api/{path}
            return string.Format("{0}://{1}{2}?{3}", scheme, host, DeterminePath(path));
        }
        else
        {
            return string.Format("{0}://{1}{2}?{3}", scheme, host, path);
        }
    }

    private static string DeterminePath(string path)
    {
        if (path == "/")
        {
            return "/api/root";
        }
        else 
        {
            return "/api" + path;
        }
    }
}
