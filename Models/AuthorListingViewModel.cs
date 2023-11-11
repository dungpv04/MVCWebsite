using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMvc.Models
{
    public class AuthorListingViewModel
    {
        public string sortRequest {  get; set; }
        public int Page { get; set; }
        public int Pager { get; set; }
        public string Keyword { get; set; } = string.Empty;
        public int lastPage;
        public int totalPage;
        public IEnumerable<AuthorViewModel> Listing { get; set; }
    }
}