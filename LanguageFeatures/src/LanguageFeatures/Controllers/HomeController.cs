using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LanguageFeatures.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {

        bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }

        //public ViewResult Index() => View(Product.GetProducts().Select(p => p?.Name));

        //public async Task<ViewResult> Index()
        //{
        //    long? length = await MyAsyncMethods.GetPageLength();
        //    return View(new string[] { $"Length: {length}" });
        //}
        public ViewResult Index()
        {
            var products = new[]
            {
                 new { Name = "Kayak", Price = 275M },
                 new { Name = "Lifejacket", Price = 48.95M },
                 new { Name = "Soccer ball", Price = 19.50M },
                 new { Name = "Corner flag", Price = 34.95M }
            };
            return View(products.Select(p => $"{nameof(p.Name)}: {p.Name}, {nameof(p.Price)}: {p.Price}"));
        }
    }
}
