
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.Models
{
    public class Diary
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("publish_date")]
        public DateTime PublishDate { get; set; }
        [JsonProperty("public")]
        public bool IsPublic { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }

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