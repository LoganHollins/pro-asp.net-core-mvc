using System;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Linq;
namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour > 12 ? "Good Afternoon" : "Good Morning";
            return View("MyView");
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (!ModelState.IsValid)
                return View();

            Repository.AddResponse(guestResponse);
            return View("Thanks", guestResponse);
        }

        [HttpGet]
        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }

    }
}
