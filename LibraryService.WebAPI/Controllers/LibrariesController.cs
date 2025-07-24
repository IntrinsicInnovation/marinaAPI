
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using System;
using System.Collections;
using System.Linq;


namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILibrariesService librariesService)
        {
            _librariesService = librariesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _librariesService.Get(null);
            return Ok(libraries);
        }

        [HttpGet("{libraryId}")]
        public async Task<IActionResult> Get(int libraryId)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Library l)
        {
            await _librariesService.Add(l);
            return Ok(l);
        }

        [HttpPut("{libraryId}")]
        public async Task<IActionResult> Update(int libraryId, Library library)
        {
            var existingLibrary = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (existingLibrary == null)
                return NotFound();

            await _librariesService.Update(library);
            return NoContent();
        }

        // Implement the DELETE method below

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteLibrary(int id)
            {
                var lib = (await _librariesService.Get(new int[] {id})).FirstOrDefault();
           
                if (lib != null)
                {
                    bool isDeleted = await _librariesService.Delete(lib);
                    if (isDeleted)
                    {
                        return NoContent();
                    }
                }
                return NotFound();
            }
    }
}
