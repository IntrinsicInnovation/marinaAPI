using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        // Implement the functionalities below
        [HttpPost]
               
        public async Task<IActionResult> AddBook(int libraryId, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();
                  


            book.LibraryId = libraryId;
            var newbook = await _booksService.Add(book);
            return CreatedAtAction(nameof(AddBook), new {id = newbook.Id}, newbook);
         }

        [HttpGet] //("{libraryId}")]
        public async Task<IActionResult> Get(int libraryId) //, int[] ids = null)
        {
            var book = await _booksService.Get(libraryId, null); //, ids);
            if (book == null) //  || book.Count() == 0)
            { 
                return NotFound();
            }
            return Ok(book);
        }

       /* [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            var books = await _booksService.Get(id, null);
            return Ok(books);
        }
       */

    }
}