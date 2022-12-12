using Microsoft.EntityFrameworkCore;
using ReadABook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.Repositories
{
    public class BooksDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookToRead> BooksToRead { get; set; }


        public BooksDbContext()
        {
            this.Users = this.Set<User>();
            this.Books = this.Set<Book>();
            this.BooksToRead = this.Set<BookToRead>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=BooksDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "admin",
                    Password = "adminpass",
                    FirstName = "Admini",
                    LastName = "Strator"
                });
        }
    }
}
