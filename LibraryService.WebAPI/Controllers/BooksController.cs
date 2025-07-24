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
               
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var newbook = await _booksService.Add(book);
            return CreatedAtAction(nameof(GetBook), new {id = newbook.Id}, newbook);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int libraryId, int[] ids)
        {
            var book = await _booksService.Get(libraryId, ids);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }




    }
}