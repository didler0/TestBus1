using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestBus1.Models
{
    public class ShortUrlModel
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Длинная (оригинальная) ссылка")]
        public string OriginalUrl { get; set; }
        

        [Display(Name = "Короткая (сокращенная) ссылка")]
        public string ShortenedUrl { get; set; }

        public DateTime CreatedDate { get;}
        public int ClickCount { get; set; }
    }
}
