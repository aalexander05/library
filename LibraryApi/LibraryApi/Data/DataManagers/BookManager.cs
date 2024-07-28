
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data.DataManagers;

public class BookManager : IBookManager
{
    private AppDbContext _context;

    public BookManager(AppDbContext context)
    {
        _context = context;
    }

    public Task<int> CreateBook(Book book)
    {
        _context.Books.Add(book);

        return _context.SaveChangesAsync();
    }

    public Task<int> DeleteBook(int id)
    {
        Book Book = _context.Books.First(x => x.Id == id);
        _context.Books.Remove(Book);

        return _context.SaveChangesAsync();
    }

    public Task<int> UpdateBook(Book Book)
    {
        _context.Books.Update(Book);

        return _context.SaveChangesAsync();
    }

    public Task<Book> GetBookById(int id)
    {
        return _context.Books.FirstAsync(x => x.Id == id);
    }

    public Task<List<Book>> GetAll()
    {
        return _context.Books
            .Include(x => x.CheckedOutByUser)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public Task<List<Book>> Search(string title)
    {
        return _context.Books
            .Where(x => string.IsNullOrEmpty(title) || x.Title.ToLower().Contains(title.ToLower()))
            .Include(x => x.CheckedOutByUser)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}
