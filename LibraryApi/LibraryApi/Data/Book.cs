using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte[]? CoverImage { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public ApplicationUser? CheckedOutByUser { get; set; }
    public DateTime? ReturnDueDate { get; set; }

}
