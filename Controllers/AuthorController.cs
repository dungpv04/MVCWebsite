﻿using DemoMvc.Models;
using MVCDemoService;
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
        public AuthorController (IAuthorService _authorService)
        {
            authorService = _authorService;

        }
        
        public ActionResult Index()
        {
            var result = authorService.SearchAuthor();
            var model = new AuthorListingViewModel() {
                Keyword = string.Empty,
                Listing = result
            };
            return View(model);
        }
        public ActionResult Create()
        {
            return View(new AuthorViewModel());
        }
        public ActionResult CreateSubmit(AuthorViewModel author)
        {
            authorService.AddAuthor(author);
            var result = authorService.SearchAuthor();
            var model = new AuthorListingViewModel() { Listing = result };
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
            var result = authorService.SearchAuthor();
            var model = new AuthorListingViewModel() { Listing = result };
            return View("Index",model);
        }
        public ActionResult Delete(int id)
        {
            authorService.DeleteAuthor(id);
            var result = authorService.SearchAuthor();
            var model = new AuthorListingViewModel() { Listing = result };
            return View("Index", model);
        }

        public ActionResult Search(string keyword) 
        {
            
            var result = authorService.SearchAuthor(keyword);
            var model = new AuthorListingViewModel()
            {
                Keyword = keyword,
                Listing = result
            };
            return View("Index", model);

        }
    }
}