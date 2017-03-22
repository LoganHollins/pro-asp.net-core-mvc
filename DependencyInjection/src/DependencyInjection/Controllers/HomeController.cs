using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DependencyInjection.Models;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index([FromServices]ProductTotalizer totalizer)
        {
            IRepository repository = (IRepository)HttpContext.RequestServices.GetService(typeof(IRepository));
            ViewBag.Totalizer = totalizer.Repository.ToString();
            return View(repository.Products);
        }
    }
}
