using DemoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMvc.Models
{
    public class BookListingViewModel
    {
        public string keyword {  get; set; }
        public IEnumerable<BookViewModel> listing { get; set; }
    }
}