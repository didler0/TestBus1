using Microsoft.AspNetCore.Mvc;
using TestBus1.Data;
using TestBus1.Models;
using System.Linq;
using System;

namespace TestBus1.Controllers
{
    public class UrlController : Controller
    {
        private readonly UrlShortenerDbContext _context;

        public UrlController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var urls = _context.ShortUrls.ToList();
            return View(urls);
        }

        public IActionResult Create()
        {
            return View();
        }


        


        [Route("Url/Delete/{id}")]
        public IActionResult Delete_POST(int id)
        {
            var url = _context.ShortUrls.Find(id);

            if (url == null)
            {
                return NotFound();
            }

            _context.ShortUrls.Remove(url);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Create(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                ModelState.AddModelError("OriginalUrl", "Поле 'Длинная ссылка' обязательно для заполнения.");
                return View();
            }

            var model = new ShortUrlModel
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = GenerateShortUrl(),
                CreatedDate = DateTime.UtcNow,
                ClickCount = 0
            };

            _context.ShortUrls.Add(model);
            _context.SaveChanges();
            Console.WriteLine(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var url = _context.ShortUrls.Find(id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        [HttpPost("Url/Edit/{id}")]
        public IActionResult Edit(ShortUrlModel model)
        {
            if (ModelState.IsValid)
            {
                _context.ShortUrls.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        private string GenerateShortUrl()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        [Route("Url/Redirect/{shortenedUrl}")]
        public IActionResult Redirect()
        {
            var shortenedUrl = this.RouteData.Values["shortenedUrl"]?.ToString();
            var url = _context.ShortUrls.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl);
            
            if (url == null)
            {
                return NotFound();
            }
            url.ClickCount++;
            _context.SaveChanges();
            
            return Redirect(url.OriginalUrl);
        }

        

    }
}