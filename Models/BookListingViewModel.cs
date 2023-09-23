﻿using DemoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMvc.Models
{
    public class BookListingViewModel
    {
        public int Page {  get; set; }
        public int Pager { get; set; }
        public string Keyword {  get; set; }
        public IEnumerable<BookViewModel> Listing { get; set; }
    }
}