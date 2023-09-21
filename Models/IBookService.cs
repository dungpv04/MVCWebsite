using DemoMvc;
using DemoMvc.Models;
using MVCWebsite;
using MVCWebsite.Desgin_Parttern;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVCDemoService
{
    public interface IBookService
    {

        bool AddBook(BookViewModel book);

        bool UpdateBook(BookViewModel book);




        bool DeleteBook(int id);


        BookViewModel GetBook(int id);

        IEnumerable<BookViewModel> SearchBook(string keyword = null, int page = 0, int size = 10);


        IEnumerable<SelectListItem> GetAuthors();


       
        

    }
}
