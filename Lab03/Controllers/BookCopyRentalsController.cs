using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab03.Data;
using Lab03.Models;

namespace Lab03.Controllers
{
    public class BookCopyRentalsController : Controller
    {
        private readonly LabContext _context;

        public BookCopyRentalsController(LabContext context)
        {
            _context = context;
        }

        // GET: BookCopyRentals
        public async Task<IActionResult> Index(string searchString)
        {
            var bookCopyRentals = await _context.BookCopyRentals.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                int i = 0;
                Int32.TryParse(searchString, out i);
                bookCopyRentals = bookCopyRentals.Where(c => c.ClientID.Equals(i)).ToList();
            }
            return View(bookCopyRentals);
        }

        // GET: BookCopyRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopyRental = await _context.BookCopyRentals
                .Include(b => b.BookCopy)
                .Include(b => b.Client)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopyRental == null)
            {
                return NotFound();
            }

            return View(bookCopyRental);
        }

        // GET: BookCopyRentals/Create
        public IActionResult Create()
        {
            ViewData["BookCopyID"] = new SelectList(_context.BookCopies, "ID", "ID");
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID");
            return View();
        }

        // POST: BookCopyRentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClientID,BookCopyID,RentalDate,RentalDueDate,RentalReturnDate,Lost")] BookCopyRental bookCopyRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCopyRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCopyID"] = new SelectList(_context.BookCopies, "ID", "ID", bookCopyRental.BookCopyID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", bookCopyRental.ClientID);
            return View(bookCopyRental);
        }

        // GET: BookCopyRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopyRental = await _context.BookCopyRentals.SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopyRental == null)
            {
                return NotFound();
            }
            ViewData["BookCopyID"] = new SelectList(_context.BookCopies, "ID", "ID", bookCopyRental.BookCopyID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", bookCopyRental.ClientID);
            return View(bookCopyRental);
        }

        // POST: BookCopyRentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClientID,BookCopyID,RentalDate,RentalDueDate,RentalReturnDate,Lost")] BookCopyRental bookCopyRental)
        {
            if (id != bookCopyRental.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCopyRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCopyRentalExists(bookCopyRental.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCopyID"] = new SelectList(_context.BookCopies, "ID", "ID", bookCopyRental.BookCopyID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "ID", bookCopyRental.ClientID);
            return View(bookCopyRental);
        }

        // GET: BookCopyRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopyRental = await _context.BookCopyRentals
                .Include(b => b.BookCopy)
                .Include(b => b.Client)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopyRental == null)
            {
                return NotFound();
            }

            return View(bookCopyRental);
        }

        // POST: BookCopyRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCopyRental = await _context.BookCopyRentals.SingleOrDefaultAsync(m => m.ID == id);
            _context.BookCopyRentals.Remove(bookCopyRental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCopyRentalExists(int id)
        {
            return _context.BookCopyRentals.Any(e => e.ID == id);
        }
    }
}
