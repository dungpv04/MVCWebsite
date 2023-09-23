using DemoMvc.Models;
using MVCDemoService;
using MVCWebsite.Desgin_Parttern;
using MVCWebsite.Models;
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
        private IAuthorService authorService;
        private IBookEntities bookEntities;
        public AuthorController (IAuthorService _authorService, IBookEntities _bookEntities)
        {
            authorService = _authorService;
            bookEntities = _bookEntities;
        }
        
        public ActionResult Index()
        {
            int Page = 0;
            var result = authorService.SearchAuthor(null, Page, 3);
            var model = new AuthorListingViewModel() {
                Page = Page,
                Pager = Page + 1,
                Listing = result
            };
            return View(model);
        }

        public ActionResult Prev(int Page)
        {
            
            if (Page > 0) Page--;
            var result = authorService.SearchAuthor(null, Page, 3);
            var model = new AuthorListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Listing = result
            };
            return View("Index",model);
        }

        public ActionResult Next(int Page)
        {

            var countQuery = bookEntities.Author.Count();
            

            if (countQuery % 3 != 0) countQuery = countQuery/ 3;

            else if(countQuery % 3 == 0) countQuery = countQuery/ 3 - 1;

            if (Page < countQuery) Page++;
            var result = authorService.SearchAuthor(null, Page, 3);
            var model = new AuthorListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Listing = result
            };
            return View("Index",model);
        }

        public ActionResult Create()
        {
            return View(new AuthorViewModel());
        }
        public ActionResult CreateSubmit(AuthorViewModel author)
        {
            authorService.AddAuthor(author);
            var result = authorService.SearchAuthor(null, 0, 3);
            var model = new AuthorListingViewModel() 
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
            var author = authorService.GetAuthor(id);
            return View(author);
        }
        public ActionResult EditSubmit(AuthorViewModel authors)
        {
            authorService.UpdateAuthor(authors);
            var result = authorService.SearchAuthor(null, 0, 3);
            var model = new AuthorListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index",model);
        }
        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            var result = authorService.SearchAuthor(null, 0, 3);
            var model = new AuthorListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result
            };
            return View("Index", model);
        }

        public ActionResult Search(string Keyword) 
        {
            int Page = 0;
            var result = authorService.SearchAuthor(Keyword, Page, 3);
            var model = new AuthorListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = Keyword,
                Listing = result
            };
            return View("Search", model);

        }

        public ActionResult PrevSearch(int Page, string Keyword)
        {

            if (Page > 0) Page--;
            var result = authorService.SearchAuthor(Keyword, Page, 3);
            var model = new AuthorListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = Keyword,
                Listing = result
            };
            return View("Search", model);
        }

        public ActionResult NextSearch(int Page, string Keyword)
        {

            var countQuery = authorService.SearchAuthor(Keyword).Count();


            if (countQuery % 3 != 0) countQuery = countQuery / 3;

            else if (countQuery % 3 == 0) countQuery = countQuery / 3 - 1;

            if (Page < countQuery) Page++;
            var result = authorService.SearchAuthor(Keyword, Page, 3);
            var model = new AuthorListingViewModel()
            {
                Page = Page,
                Pager = Page + 1,
                Keyword = Keyword,
                Listing = result
            };
            return View("Search", model);
        }


    }
}