using DemoMvc.Models;
using MVCWebsite;
using MVCWebsite.Desgin_Parttern;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public IEnumerable<BookViewModel> SearchBook(string keyword = null, int page = 0, int size = 3, string sortRequest = null)
        {
            var bookQuery = bookEntities.Book.AsQueryable();
            if (keyword != "" && keyword != null)
            {
                bookQuery = bookQuery.Where(x => x.Content.Contains(keyword) || x.Name.Contains(keyword) || (x.Author != null && x.Author.Name.Contains(keyword)));
            }

            IEnumerable<BookViewModel> result = bookQuery.Select(x => new BookViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Content = x.Content,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.Name != null ? x.Author.Name : null,
            });

            switch (sortRequest)
            {
                case "NameDesc":
                    result = result.OrderByDescending(x => x.Name).Skip(size * page).Take(size);
                    break;

                case "IdAsc":
                    result = result.OrderBy(x => x.Id).Skip(size * page).Take(size);
                    break;

                case "IdDesc":
                    result = result.OrderByDescending(x => x.Id).Skip(size * page).Take(size);
                    break;

                case "ContentAsc":
                    result = result.OrderBy(x => x.Content).Skip(size * page).Take(size);
                    break;

                case "ContentDesc":
                    result = result.OrderByDescending(x => x.Content).Skip(size * page).Take(size);
                    break;

                case "AuthorNameAsc":
                    result = result.OrderBy(x => x.AuthorName).Skip(size * page).Take(size);
                    break;

                case "AuthorNameDesc":
                    result = result.OrderByDescending(x => x.AuthorName).Skip(size * page).Take(size);
                    break;

                case "AuthorIdAsc":
                    result = result.OrderBy(x => x.AuthorId).Skip(size * page).Take(size);
                    break;

                case "AuthorIdDesc":
                    result = result.OrderByDescending(x => x.AuthorId).Skip(size * page).Take(size);
                    break;

                default:
                    result = result.OrderBy(x => x.Name).Skip(size * page).Take(size);
                    break;
            }

            return result.ToList();
        }

        public IEnumerable<SelectListItem> GetAuthors()
        {
            return bookEntities.Author.ToList().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
        }

        public int LastPageUpdate(string keyword = null)
        {
            int totalRecord;
            int lastPage;
            if (keyword != null) totalRecord = bookEntities.Book.Where(x => x.Content.Contains(keyword) || x.Name.Contains(keyword) || (x.Author != null && x.Author.Name.Contains(keyword))).Count();
            else totalRecord = bookEntities.Book.Count();

            if (totalRecord % 3 == 0) lastPage = totalRecord / 3 - 1;
            else lastPage = totalRecord / 3;

            return lastPage;
        }

    }
}
