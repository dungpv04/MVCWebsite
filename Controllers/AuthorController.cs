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
    [Authorize(Roles = "User, Admin")]
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
        
        
        public ActionResult Index(string sortRequest = "", int Page = 0, string keyword = null)
        {
            
            var result = authorService.SearchAuthor(keyword, Page, 3, sortRequest);
            
            var model = new AuthorListingViewModel() {
                sortRequest = sortRequest,
                Page = Page,
                Pager = Page + 1,
                Listing = result,
                Keyword = keyword,
                lastPage = authorService.LastPageUpdate(keyword),
                totalPage = authorService.LastPageUpdate(keyword) + 1
            };
            
            
            return View(model);
        }

        public ActionResult Prev(int Page, string sortRequest, string keyword = null)
        {
            
            if (Page > 0) Page--;
            var result = authorService.SearchAuthor(keyword, Page, 3, sortRequest);
            var model = new AuthorListingViewModel()
            {
                sortRequest = sortRequest,
                Page = Page,
                Pager = Page + 1,
                Listing = result,
                Keyword = keyword,
                lastPage = authorService.LastPageUpdate(keyword),
                totalPage = authorService.LastPageUpdate(keyword) + 1

            };
            return View("Index",model);
        }

        public ActionResult Next(int Page, string sortRequest, string keyword = null)
        {

            if (Page < authorService.LastPageUpdate(keyword)) Page++;
            var result = authorService.SearchAuthor(keyword, Page, 3, sortRequest);
            var model = new AuthorListingViewModel()
            {
                sortRequest = sortRequest,
                Page = Page,
                Pager = Page + 1,
                Listing = result,
                Keyword = keyword,
                lastPage = authorService.LastPageUpdate(keyword),
                totalPage = authorService.LastPageUpdate(keyword) + 1
                
            };
            return View("Index",model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new AuthorViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateSubmit(AuthorViewModel author)
        {
            authorService.AddAuthor(author);
            var result = authorService.SearchAuthor(null, 0, 3);
            var model = new AuthorListingViewModel() 
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = authorService.LastPageUpdate(),
                totalPage = authorService.LastPageUpdate()+ 1
            };
            return View("Index", model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var author = authorService.GetAuthor(id);
            return View(author);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditSubmit(AuthorViewModel authors)
        {
            authorService.UpdateAuthor(authors);
            var result = authorService.SearchAuthor(null, 0, 3);
            var model = new AuthorListingViewModel()
            {
                Page = 0,
                Pager = 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = authorService.LastPageUpdate(),
                totalPage = authorService.LastPageUpdate() + 1
            };
            return View("Index",model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, int currentPage)
        {
            authorService.DeleteAuthor(id);
            var result = authorService.SearchAuthor(null, currentPage, 3);
            var model = new AuthorListingViewModel()
            {
                Page = currentPage,
                Pager = currentPage + 1,
                Keyword = string.Empty,
                Listing = result,
                lastPage = authorService.LastPageUpdate(),
                totalPage = authorService.LastPageUpdate() + 1
            };
            return View("Index", model);
        }

    }
}