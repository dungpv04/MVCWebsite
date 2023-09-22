using DemoMvc.Models;
using MVCDemoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMvc.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private IBookService bookService;
        private IEnumerable<SelectListItem> authors;
        public BookController(IBookService _bookService)
        {
            bookService = _bookService;
            authors = bookService.GetAuthors();
        }
        public ActionResult Index()
        {
            var result = bookService.SearchBook();
            var model = new BookListingViewModel()
            {
                keyword = string.Empty,
                listing = result
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new BookViewModel() { Authors = authors});
        }

        public ActionResult CreateSubmit(BookViewModel book)
        {
            bookService.AddBook(book);
            var result = bookService.SearchBook();
            var model = new BookListingViewModel() { listing = result };
            return View("Index", model);
        }

        public ActionResult Edit(int id)
        {
            var book = bookService.GetBook(id);
            book.Authors = authors;
            return View(book);
        }

        public ActionResult EditSubmit(BookViewModel book)
        {
            bookService.UpdateBook(book);
            var result = bookService.SearchBook();
            var model = new BookListingViewModel { listing = result };
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            var result = bookService.SearchBook();
            var model = new BookListingViewModel { listing = result };
            return View("Index",model);
        }

        public ActionResult Search(string keyword)
        {

            var result = bookService.SearchBook(keyword);
            var model = new BookListingViewModel() 
            {
                keyword = keyword,
                listing = result
            };
            return View("Index", model);
        }
        
    }
}