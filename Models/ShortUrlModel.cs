using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestBus1.Models
{
    public class ShortUrlModel
    {
        public int Id { get; set; }

        [Display(Name = "Длинная (оригинальная) ссылка")]
        [Required(ErrorMessage = "Поле 'Длинная ссылка' обязательно для заполнения.")]
        [Url(ErrorMessage = "Введите корректный URL.")]
        public string OriginalUrl { get; set; }

        [Display(Name = "Сокращенная ссылка")]
        [Required(ErrorMessage = "Поле 'Сокращенная ссылка' обязательно для заполнения.")]
        public string ShortenedUrl { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Количество переходов")]
        public int ClickCount { get; set; }
    }
}
