using DemoMvc.Models;
using MVCDemoService;
using MVCWebsite.Models;
using MVCWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebsite.Desgin_Parttern;

namespace DemoMvc.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private IBookService bookService;
        private IBookEntities bookEntities;
        private IEnumerable<SelectListItem> authors;
        public BookController(IBookService _bookService, IBookEntities _bookEntities)
        {
            bookService = _bookService;
            bookEntities = _bookEntities;
            authors = bookService.GetAuthors();
        }
        public ActionResult Index()
        {
            int Page = 0;
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View(model);
        }

        public ActionResult Prev(int Page)
        {

            if (Page > 0) Page--;
            var result = bookService.SearchBook(null, Page, 3);
            var model = new BookListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index", model);
        }

        public ActionResult Next(int Page)
        {

            var countQuery = bookEntities.Book.Count();


            if (countQuery % 3 != 0) countQuery = countQuery / 3;

            else if (countQuery % 3 == 0) countQuery = countQuery / 3 - 1;

            if (Page < countQuery) Page++;
            var result = bookService.SearchBook(null, Page, 3);
            var model = new BookListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index", model);
        }

        public ActionResult Create()
        {
            return View(new BookViewModel() { Authors = authors});
        }

        public ActionResult CreateSubmit(BookViewModel book)
        {
            bookService.AddBook(book);
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result
            };
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
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index",model);
        }

        public ActionResult Search(string keyword)
        {

            var result = bookService.SearchBook(keyword);
            var model = new BookListingViewModel() 
            {
                Keyword = keyword,
                Listing = result
            };
            return View("Index", model);
        }
        
    }
}