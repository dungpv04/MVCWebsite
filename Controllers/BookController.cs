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
    [Authorize(Roles = "User, Admin")]
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
        public ActionResult Index(string sortRequest, int Page = 0, string keyword = null)
        {
            
            var result = bookService.SearchBook(keyword, Page, 3, sortRequest);
            var model = new BookListingViewModel()
            {
                sortRequest = sortRequest,
                Page = Page,
                Pager = Page + 1,
                Keyword = keyword,
                Listing = result,
                lastPage = bookService.LastPageUpdate(keyword),
                totalPage = bookService.LastPageUpdate(keyword) + 1
            };
            return View(model);
        }

        public ActionResult Prev(int Page, string sortRequest, string keyword = null)
        {

            if (Page > 0) Page--;
            var result = bookService.SearchBook(keyword, Page, 3, sortRequest);
            var model = new BookListingViewModel()
            {
                sortRequest= sortRequest,
                Page = Page,
                Pager = Page + 1,
                Keyword = keyword,
                Listing = result,
                lastPage = bookService.LastPageUpdate(keyword),
                totalPage = bookService.LastPageUpdate(keyword) + 1
            };
            return View("Index", model);
        }

        public ActionResult Next(int Page, string sortRequest, string keyword = null)
        {

            if (Page < bookService.LastPageUpdate(keyword)) Page++;
            var result = bookService.SearchBook(keyword, Page, 3, sortRequest);
            var model = new BookListingViewModel()
            {
                sortRequest = sortRequest,
                Page = Page,
                Pager = Page + 1,
                Keyword = keyword,
                Listing = result,
                lastPage = bookService.LastPageUpdate(keyword),
                totalPage = bookService.LastPageUpdate(keyword) + 1
            };
            return View("Index",model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new BookViewModel() { Authors = authors});
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateSubmit(BookViewModel book)
        {
            bookService.AddBook(book);
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = bookService.LastPageUpdate(),
                totalPage = bookService.LastPageUpdate() + 1
            };
            return View("Index", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var book = bookService.GetBook(id);
            book.Authors = authors;
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditSubmit(BookViewModel book)
        {
            bookService.UpdateBook(book);
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = bookService.LastPageUpdate(),
                totalPage = bookService.LastPageUpdate() + 1
            };
            return View("Index", model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int currentPage)
        {
            bookService.DeleteBook(id);
            var result = bookService.SearchBook(null, 0, 3);
            var model = new BookListingViewModel()
            {
                Page = currentPage,
                Pager = currentPage + 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = bookService.LastPageUpdate(),
                totalPage = bookService.LastPageUpdate() + 1
            };
            return View("Index",model);
        }
        public ActionResult Settitle(string Keyword)
        {
            Session["Title"] = Keyword;
            HttpCookie cookie = new HttpCookie("Title", Keyword);
            cookie.Expires = DateTime.Now.AddSeconds(30);
            Response.Cookies.Add(cookie);
            int Page = 0;
            var result = bookService.SearchBook(Keyword, Page, 3);
            var model = new BookListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = Keyword,
                Listing = result
            };
            return View("Index", model);
        }

    }
}