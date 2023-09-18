using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebsite.Desgin_Parttern
{
    public interface IBookEntities
    {
        DbSet<Author> Author { get; set; }
        DbSet<Book> Book { get; set; }
        int SaveChanges();
    }
}