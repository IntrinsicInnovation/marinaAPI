using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Services
{
    public class BooksService : IBooksService
    {
        private readonly LibraryContext _libraryContext;

        public BooksService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }




        public async Task<IEnumerable<Book>> Get(int libraryId, int[] ids)
        {

            //return _books.FirstOrDefault(b => b.Id == id);

            var allbooks =  _libraryContext.Books.AsQueryable().Where(b => b.LibraryId == libraryId);


           // var allbooks = await _libraryContext.Books.Where(b => b.LibraryId == libraryId);

           // if (ids != null && ids.Any())
           //     allbooks = allbooks.Where(x => ids.Contains(x.Id) && x.LibraryId == libraryId);

            return await allbooks.ToListAsync();
        }

        public async Task<Book> Add(Book book)
        {
            
            

            await _libraryContext.Books.AddAsync(book);

            await _libraryContext.SaveChangesAsync();
            return book;


        }

        public async Task<Book> Update(Book book)
        {
            // Complete the implementation
             var projectForChanges = await _libraryContext.Books.SingleAsync(x => x.Id == book.Id);
            projectForChanges.Name = book.Name;
            projectForChanges.Category = book.Name;

            _libraryContext.Books.Update(projectForChanges);
            await _libraryContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(Book book)
        {
            // Complete the implementation
            
            var booktodelete = await _libraryContext.Books.SingleAsync(x => x.Id == book.Id);
            if (booktodelete == null)
                return false;

            _libraryContext.Books.Remove(book);

            return true;

        }
    }

    public interface IBooksService
    {
        Task<IEnumerable<Book>> Get(int libraryId, int[] ids);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);

        Task<bool> Delete(Book book);
    }
}
