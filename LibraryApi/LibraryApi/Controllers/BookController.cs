using AutoMapper;
using LibraryApi.Data;
using LibraryApi.Data.DataManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Authorize(AuthenticationSchemes = "Identity.Bearer", Policy = "LibrarianPolicy")]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookManager _bookManager;
    private readonly IMapper _mapper;

    public BookController(IBookManager BookManager, IMapper mapper)
    {
        _bookManager = BookManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<DTOs.Book>> CreateBook([FromBody] DTOs.Book book)
    {
        Book newBook = _mapper.Map<Book>(book);

        await _bookManager.CreateBook(newBook);

        DTOs.Book bookDto = _mapper.Map<DTOs.Book>(newBook);

        return CreatedAtAction("GetBookById", new { id = bookDto.Id }, bookDto);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookManager.DeleteBook(id);
        return NoContent();
    }


    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromBody] DTOs.Book book)
    {
        Book bookToUpdate = await _bookManager.GetBookById(book.Id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        _mapper.Map(book, bookToUpdate);

        await _bookManager.UpdateBook(bookToUpdate);
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetBookById")]
    public async Task<ActionResult<DTOs.Book>> GetBookById(int id)
    {
        Book book = await _bookManager.GetBookById(id);

        DTOs.Book bookDto = _mapper.Map<DTOs.Book>(book);

        return Ok(bookDto);
    }
}
