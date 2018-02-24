
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.Data.Models
{
    public class Diary
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("author")]
        public string Author;
        [JsonProperty("publish_date")]
        public DateTime PublishDate;
        [JsonProperty("public")]
        public bool IsPublic;
        [JsonProperty("text")]
        public string Text;

        public static List<Diary> Generate(bool publicOnly = false)
        {
            Random r = new Random();
            int entries = r.Next(0,10);
            List<int> ids = new List<int>();
            for (int i = 0; i < entries;) 
            {
                int id = r.Next(1, 1000);
                if (!ids.Contains(id)) {
                    ids.Add(id);
                    i++;
                }
            }

            List<Diary> diaries = new List<Diary>();
            for (int i = 0; i < ids.Count; i++) 
            {
                diaries.Add(
                    new Diary()
                    {
                        Id = ids[i],
                        Title = "title for " + ids[i],
                        Author = publicOnly ? "author for " + ids[i] : "myself",
                        PublishDate = DateTime.Now,
                        IsPublic = ids[i] % 2 == 0 || publicOnly,
                        Text = "text for " + ids[i]
                    }
                );
            }

            return diaries;
        }
    }
}