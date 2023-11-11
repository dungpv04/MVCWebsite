using DemoMvc.Models;
using MVCWebsite.Desgin_Parttern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebsite.Models
{
    public interface IAuthorService
    {
        bool AddAuthor(AuthorViewModel authors);

        bool DeleteAuthor(int Id);

        bool UpdateAuthor(AuthorViewModel authors);
        AuthorViewModel GetAuthor(int Id);
        IEnumerable<AuthorViewModel> SearchAuthor(string keyword = null, int page = 0, int size = 10, string sortRequest = "Ascending");
        int LastPageUpdate(string keyword = null);

    }
}