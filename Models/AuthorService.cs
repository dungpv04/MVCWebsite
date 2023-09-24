using DemoMvc.Models;
using MVCWebsite.Desgin_Parttern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebsite.Models
{
    public class AuthorService: IAuthorService
    {

        private IBookEntities bookEntities;


        public AuthorService(IBookEntities _bookEntities)
        {
            bookEntities = _bookEntities;
        }
        public bool AddAuthor(AuthorViewModel authors)
        {

            bookEntities.Author.Add(new Author
            {
                Name = authors.Name

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

        public IEnumerable<AuthorViewModel> SearchAuthor(string keyword = null, int page = 0, int size = 3, string sortRequest = null)
        {
            var bookQuery = bookEntities.Author.AsQueryable();
            if (keyword != null)
            {
                bookQuery = bookQuery.Where(x => x.Name.Contains(keyword));
            }

            IEnumerable<AuthorViewModel> result = bookQuery.Select(x => new AuthorViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            switch(sortRequest)
            {
                case "Name":
                    result = result.OrderByDescending(x => x.Name).Skip(size * page).Take(size); 
                    break;

                case "IdAsc":
                    result = result.OrderBy(x => x.Id).Skip(size * page).Take(size);
                    break;

                case "IdDesc":
                    result = result.OrderByDescending(x => x.Id).Skip(size * page).Take(size);
                    break;
                    
                default:
                    result = result.OrderBy(x => x.Name).Skip(size * page).Take(size);
                    break;
            }
            
            return result.ToList();

        }
        public AuthorViewModel GetAuthor(int id)
        {
            var a = bookEntities.Author.Where(x => x.Id == id).Select(x => new AuthorViewModel()
            {
                Id = id,
                Name = x.Name,
            }).FirstOrDefault();
            return a;
        }
    }
}