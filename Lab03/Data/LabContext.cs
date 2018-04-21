using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab03.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab03.Data
{
    public class LabContext : DbContext
    {
        public LabContext(DbContextOptions<LabContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BookCopyRental> BookCopyRentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<BookCopy>().ToTable("BookCopy");
            modelBuilder.Entity<BookCopyRental>().ToTable("BookCopyRental");
        }

    }
}
