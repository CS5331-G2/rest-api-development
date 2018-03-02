using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace diary.Models
{
    public class PostSummaryViewModel : PageModel
    {
        public IEnumerable<PostSummaryModel> PostSummaries { get; set; }

        public class PostSummaryModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime PublishDate { get; set; }
            public bool IsPublic { get; set; }
            public string Text { get; set; }
        }
    }
}
