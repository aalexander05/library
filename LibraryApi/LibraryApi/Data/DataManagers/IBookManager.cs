namespace LibraryApi.Data.DataManagers;

public interface IBookManager
{
    Task<int> CreateBook(Book book);
    Task<int> DeleteBook(int id);
    Task<int> UpdateBook(Book book);

    Task<Book> GetBookById(int id);
    Task<List<Book>> GetAll();
    Task<List<Book>> Search(string title);
}
