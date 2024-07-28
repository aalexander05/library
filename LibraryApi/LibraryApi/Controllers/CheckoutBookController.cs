using AutoMapper;
using LibraryApi.Data;
using LibraryApi.Data.DataManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Authorize(AuthenticationSchemes = "Identity.Bearer")]
[Route("[controller]")]
public class CheckoutBookController : ControllerBase
{
    private readonly IBookManager _bookManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public CheckoutBookController(IBookManager bookManager, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _bookManager = bookManager;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CheckoutBook(int id)
    {
        Book book = await _bookManager.GetBookById(id);

        if (book == null)
        {
            return NotFound();
        }

        if (book.CheckedOutByUser != null)
        {
            return BadRequest("Cannot check out book that is already checked out.");
        }

        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user != null) { 
            book.CheckedOutByUser = user;
            book.ReturnDueDate = DateTime.UtcNow.AddDays(5);
            await _bookManager.UpdateBook(book);
        }
        else
        {
            return Problem("Could not find user.");
        }

        return NoContent();

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DTOs.Book>> GetBookById(int id)
    {
        Book book = await _bookManager.GetBookById(id);

        DTOs.Book bookDto = _mapper.Map<DTOs.Book>(book);

        return Ok(bookDto);
    }

    [HttpGet("GetAllBooks")]
    public async Task<ActionResult<DTOs.Book>> GetAllBooks()
    {
        IEnumerable<Book> books = await _bookManager.GetAll();

        IEnumerable<DTOs.Book> bookDtos = books.Select(x => _mapper.Map<DTOs.Book>(x));

        return Ok(bookDtos);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<DTOs.Book>> SearchBooks([FromQuery] string title) 
    {
        IEnumerable<Book> books = await _bookManager.Search(title);

        IEnumerable<DTOs.Book> bookDtos = books.Select(x => _mapper.Map<DTOs.Book>(x));

        return Ok(bookDtos);
    }
}
