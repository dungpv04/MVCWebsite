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
        [DisplayName("ID sách")]
        public int Id { get; set; }

        [DisplayName("Tên sách")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [DisplayName("ID tác giả")]
        public int? AuthorId { get; set; }
        [DisplayName("Tên tác giả")]
        [Required]
        public string AuthorName { get; set; }
        
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}