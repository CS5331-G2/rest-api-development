using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace diary.Models
{
    public class DiaryPost
    {
        private static List<string> reservedChars = new List<string> { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Text { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public string GetLink()
        {
            return $"/Diary/Index/{Title}/";
        }

        public static string CreateSlug(string title)
        {
            title = title.ToLowerInvariant().Replace(" ", "-");
            title = RemoveDiacritics(title);
            title = RemoveReservedUrlChars(title);

            return title.ToLowerInvariant();
        }

        private static string RemoveReservedUrlChars(string text)
        {
            foreach (var chr in reservedChars)
            {
                text = text.Replace(chr, "");
            }

            return text;
        }

        private static string RemoveDiacritics(string text)
        {
            var nString = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in nString)
            {
                var unicode = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicode != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
