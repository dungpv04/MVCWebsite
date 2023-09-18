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
        private IBookService bookService;
        private IEnumerable<SelectListItem> authors;
        public AuthorController (IBookService _bookService)
        {
            bookService = _bookService;
            authors = bookService.GetAuthors();
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