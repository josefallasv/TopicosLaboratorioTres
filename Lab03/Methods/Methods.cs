using Lab03.Models;
using Lab03.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab03.Methods
{
    public class Methods
    {
        private readonly LabContext _context;

        public Methods(LabContext context)
        {
            _context = context;
        }

        public List<Client> Method1(string name) //Clientes por nombre
        {
            var clients = _context.Clients.ToList();
            var result = clients.Where(c => c.Name.Contains(name)).ToList();
            return result;
        }

        public List<BookCopy> Method2(string name) //Ejemplares por nombre original o traducido del libro
        {
            var bookCopies = _context.BookCopies.ToList();
            var result = bookCopies.Where(c => c.Book.OriginalTitle.Contains(name) || c.Book.TranslatedTitle.Contains(name)).ToList();
            return result;
        }

        public List<Book> Method3(int id) // Libros por identificador
        {
            var books = _context.Books.ToList();
            var result = books.Where(c => c.ID.Equals(id)).ToList();
            return result;
        }

        public List<BookCopy> Method4(int id) // Ejemplares por identificador
        {
            var bookCopies = _context.BookCopies.ToList();
            var result = bookCopies.Where(c => c.ID.Equals(id)).ToList();
            return result;
        }

        public List<BookCopyRental> Method5() // Ejemplares no devueltos con entrega no vencida
        {
            var bookCopyRentals = _context.BookCopyRentals.ToList();
            var result = new List<BookCopyRental>();
            DateTime currentDate = DateTime.Today;
            foreach (BookCopyRental bcr in bookCopyRentals)
            {
                if ((DateTime.Compare(currentDate, bcr.RentalDueDate) < 0) && (bcr.RentalReturnDate == null))
                {
                    result.Add(bcr);
                }
            };
            return result;
        }

        public List<BookCopyRental> Method6() // Ejemplares no devueltos con entrega vencida
        {
            var bookCopyRentals = _context.BookCopyRentals.ToList();
            var result = new List<BookCopyRental>();
            DateTime currentDate = DateTime.Today;
            foreach (BookCopyRental bcr in bookCopyRentals)
            {
                if ((DateTime.Compare(currentDate, bcr.RentalDueDate) > 0) && (bcr.RentalReturnDate == null))
                {
                    result.Add(bcr);
                }
            };
            return result;
        }

        public List<BookCopyRental> Method7(int client) // Ejemplares solicitados por cliente
        {
            var bookCopyRentals = _context.BookCopyRentals.ToList();
            var result = bookCopyRentals.Where(b => b.ClientID.Equals(client)).ToList();
            return result;
        }

        public List<Client> Method8(int bookCopyRental) // Clientes que solicitaron un ejemplar especifico
        {
            var bookCopyRentals = _context.BookCopyRentals.Where(b => b.ID.Equals(bookCopyRental));
            var existingClients = _context.Clients.ToList();
            var result = new List<Client>();
            foreach (BookCopyRental bcr in bookCopyRentals)
            {
                if (!result.Exists(r => r.ID.Equals(bcr.ClientID)))
                {
                    result.Add(existingClients.First(e => e.ID.Equals(bcr.ClientID)));
                }      
            }
            return result;
        }
    }
}
