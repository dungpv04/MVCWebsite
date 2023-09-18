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
    public class BookService:IBookService
    {
        private IBookEntities bookEntities;
        

        public BookService(IBookEntities _bookEntities)
        {
            bookEntities = _bookEntities;
        }

        public bool AddBook(BookViewModel book)
        {
            bookEntities.Book.Add(new Book()
            {
                AuthorId = book.AuthorId,
                Name = book.Name,
                Content = book.Content
            });
            return bookEntities.SaveChanges() > 1;
        }

        public bool UpdateBook(BookViewModel book)
        {
            var b = bookEntities.Book.FirstOrDefault(x => x.Id == book.Id);
            b.Name = book.Name;
            b.Content = book.Content;
            b.AuthorId = book.AuthorId;
            return bookEntities.SaveChanges() > 1;
        }

        public bool DeleteBook(int id)
        {
            var bk = bookEntities.Book.FirstOrDefault(b => b.Id == id);
            bookEntities.Book.Remove(bk);
            return bookEntities.SaveChanges() > 1;
        }

        public BookViewModel GetBook(int id)
        {
            var b = bookEntities.Book.Where(x=>x.Id==id).Select(x =>new BookViewModel()
            {
                AuthorId=x.AuthorId,
                Name=x.Name,
                Content = x.Content,
                Id = id
            }).FirstOrDefault();
            return b;
        }

        public IEnumerable<BookViewModel> SearchBook(string keyword = null, int page = 0, int size = 10)
        {
            var bookQuery = bookEntities.Book.AsQueryable();
            if (keyword != null)
            {
                bookQuery.Where(x => x.Content.Contains(keyword) || x.Name.Contains(keyword) || (x.Author != null && x.Author.Name.Contains(keyword)));
            }
            return bookQuery.Select(x=> new BookViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Content = x.Content,
                AuthorId=x.AuthorId,
                AuthorName = x.Author!=null ? x.Author.Name : null,
            }).OrderBy(x=>x.Name).Skip(page * size).Take(size).ToList();
        }

        public IEnumerable<SelectListItem> GetAuthors()
        {
            return bookEntities.Author.ToList().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
        }

        public bool AddAuthor(AuthorViewModel authors)
        {

            bookEntities.Author.Add(new Author
            {

                Id= authors.Id,
                Name = authors.Name,

            });

            return bookEntities.SaveChanges() > 1;
        }

        public bool DeleteAuthor(int Id)
        {
            var author = bookEntities.Author.FirstOrDefault(a => a.Id == Id);
            bookEntities.Author.Remove(author);
            return bookEntities.SaveChanges() > 1;
        }

        public bool UpdateAuthor(AuthorViewModel authors)
        {
            var au = bookEntities.Author.FirstOrDefault(a => a.Id == authors.Id);
            au.Name = authors.Name;
            
            return bookEntities.SaveChanges() > 1;
        }

        public IEnumerable<AuthorViewModel> SearchAuthor(string keyword = null, int page = 0, int size = 10)
        {
            var bookQuery = bookEntities.Author.AsQueryable();
            if (keyword != null)
            {
                bookQuery.Where(x => x.Name.Contains(keyword));
            }
            return bookQuery.Select(x => new AuthorViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).OrderBy(x => x.Name).Skip(page * size).Take(size).ToList();
        }

    }
}
