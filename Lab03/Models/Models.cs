using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab03.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public ICollection<BookCopyRental> BookCopyRentals { get; set; }
    }

    public class Book
    {
        public int ID { get; set; }
        public string OriginalTitle { get; set; }
        public string TranslatedTitle { get; set; }
        public int EditionYear { get; set; }
        public int EditionNumber { get; set; }

        public ICollection<BookCopy> BookCopies { get; set; }
    }

    public class BookCopy
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public DateTime DonationDate { get; set; }
        public bool AvailableForRental { get; set; }
        public string Language { get; set; }

        public Book Book { get; set; }
        public ICollection<BookCopyRental> BookCopyRentals { get; set; }
    }

    public class BookCopyRental
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int BookCopyID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime RentalDueDate { get; set; }
        public DateTime RentalReturnDate { get; set; }
        public bool Lost { get; set; }

        public Client Client { get; set; }
        public BookCopy BookCopy { get; set; }
    }
}

