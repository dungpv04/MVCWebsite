using DemoMvc.Models;
using MVCDemoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebsite.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        private BookService bookService;
        private IEnumerable<SelectListItem> authors;
        public AuthorController()
        {
            bookService = new BookService();
        }
        public ActionResult Index()
        {
            var result = bookService.SearchAuthor();
            return View(result);
        }
        public ActionResult Create()
        {
            return View(new AuthorViewModel());
        }
    }
}