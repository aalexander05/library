using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Description = "It was published in July 1960. It is widely read in high schools and middle schools.",
                CoverImage = null,
                Publisher = "",
                PublicationDate = new DateTime(1960, 6, 11),
                Category = "Novel",
                Isbn = "978-0446310789",
                PageCount = 281
            },
            new Book
            {
                Id = 2,
                Title = "Nineteen Eighty-Four",
                Author = "George Orwell",
                Description = "Thematically, it centres on the consequences of totalitarianism, mass surveillance, and repressive regimentation of people and behaviours within society.",
                CoverImage = null,
                Publisher = "",
                PublicationDate = new DateTime(1949, 6, 8),
                Category = "Novel",
                Isbn = "978-1443434973",
                PageCount = 328
            }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                Name = "Librarian",
                NormalizedName = "LIBRARIAN",

            },
            new IdentityRole
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                Name = "Customer",
                NormalizedName = "CUSTOMER",

            }
        );

        base.OnModelCreating(modelBuilder);

    }

}

