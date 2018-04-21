using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab03.Models;

namespace Lab03.Data
{
    public class DbInitializer
    {
        public static void Initialize(LabContext context)
        {
            context.Database.EnsureCreated();

            //CLIENTES
            if (context.Clients.Any())
            {
                return;
            }

            var clients = new Client[]
            {
                new Client {Name = "Jose", Gender = "M", DateOfBirth = DateTime.Parse("2001-08-12") },
                new Client {Name = "Esteban", Gender = "M", DateOfBirth = DateTime.Parse("1999-02-12") },
                new Client {Name = "Maria", Gender = "F", DateOfBirth = DateTime.Parse("1995-03-02") },
                new Client {Name = "Viviana", Gender = "F", DateOfBirth = DateTime.Parse("1990-02-12") },
                new Client {Name = "Luis", Gender = "M", DateOfBirth = DateTime.Parse("1992-01-12") },
                new Client {Name = "Daniela", Gender = "F", DateOfBirth = DateTime.Parse("1985-10-12") }
            };

            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            //LIBROS
            if (context.BookCopies.Any())
            {
                return;
            }

            var books = new Book[]
            {
                new Book {OriginalTitle = "Book1", TranslatedTitle = "Libro1", EditionNumber = 1, EditionYear = 1990 },
                new Book {OriginalTitle = "Book2", TranslatedTitle = "Libro2", EditionNumber = 2, EditionYear = 1920 },
                new Book {OriginalTitle = "Book3", TranslatedTitle = "Libro3", EditionNumber = 3, EditionYear = 2004 },
                new Book {OriginalTitle = "Book4", TranslatedTitle = "Libro4", EditionNumber = 3, EditionYear = 2009 },
                new Book {OriginalTitle = "Book5", TranslatedTitle = "Libro5", EditionNumber = 2, EditionYear = 1987 },
                new Book {OriginalTitle = "Book6", TranslatedTitle = "Libro6", EditionNumber = 1, EditionYear = 2018 }
            };

            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();

            //EJEMPLARES
            if (context.BookCopies.Any())
            {
                return;
            }

            var bookCopies = new BookCopy[]
            {
                new BookCopy { BookID = 1, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Espanol"},
                new BookCopy { BookID = 2, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Portugues"},
                new BookCopy { BookID = 3, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Aleman"},
                new BookCopy { BookID = 4, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Mandarin"},
                new BookCopy { BookID = 5, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Frances"},
                new BookCopy { BookID = 6, DonationDate = DateTime.Parse("2018-04-21"), AvailableForRental = true, Language = "Ingles"}
            };

            foreach (BookCopy bc in bookCopies)
            {
                context.BookCopies.Add(bc);
            }
            context.SaveChanges();

            //PRESTAMO EJEMPLARES
            if (context.BookCopyRentals.Any())
            {
                return;
            }

            var bookCopyRentals = new BookCopyRental[]
            {
                new BookCopyRental {BookCopyID = 1, ClientID = 1, RentalDate = DateTime.Parse("2018-03-01"), RentalDueDate = DateTime.Parse("2018-04-11"), RentalReturnDate = DateTime.Parse("2018-04-10"), Lost = false},
                new BookCopyRental {BookCopyID = 2, ClientID = 2, RentalDate = DateTime.Parse("2018-02-02"), RentalDueDate = DateTime.Parse("2018-04-12"), RentalReturnDate = DateTime.Parse("2018-04-11"), Lost = false},
                new BookCopyRental {BookCopyID = 3, ClientID = 3, RentalDate = DateTime.Parse("2018-01-03"), RentalDueDate = DateTime.Parse("2018-04-13"), RentalReturnDate = DateTime.Parse("2018-04-12"), Lost = false},
                new BookCopyRental {BookCopyID = 4, ClientID = 4, RentalDate = DateTime.Parse("2018-01-04"), RentalDueDate = DateTime.Parse("2018-04-14"), RentalReturnDate = DateTime.Parse("2018-04-13"), Lost = false},
                new BookCopyRental {BookCopyID = 5, ClientID = 5, RentalDate = DateTime.Parse("2018-02-05"), RentalDueDate = DateTime.Parse("2018-04-15"), RentalReturnDate = DateTime.Parse("2018-04-14"), Lost = false},
                new BookCopyRental {BookCopyID = 6, ClientID = 6, RentalDate = DateTime.Parse("2018-03-06"), RentalDueDate = DateTime.Parse("2018-04-16"), RentalReturnDate = DateTime.Parse("2018-04-15"), Lost = false}
            };

            foreach (BookCopyRental bcr in bookCopyRentals)
            {
                context.BookCopyRentals.Add(bcr);
            }
            context.SaveChanges();
        }
    }
}
