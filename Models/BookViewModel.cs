using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DemoMvc.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        [DisplayName("Tên tác giả")]
        [Required]
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}