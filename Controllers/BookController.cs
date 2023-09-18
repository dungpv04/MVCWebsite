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
        private BookService bookService;
        private IEnumerable<SelectListItem> authors;
        public BookController()
        {
            bookService = new BookService();
            authors = bookService.GetAuthors();
        }
        public ActionResult Index()
        {
            var result = bookService.SearchBook();
            return View(result);
        }

        public ActionResult Create()
        {
            return View(new BookViewModel() { Authors = authors});
        }

        public ActionResult CreateSubmit(BookViewModel book)
        {
            bookService.AddBook(book);
            var result = bookService.SearchBook();
            return View("Index", result);
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
            return View("Index", result);
        }

        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            var result = bookService.SearchBook();
            return View("Index",result);
        }
    }
}