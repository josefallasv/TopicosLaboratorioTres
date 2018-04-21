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
    public class BookCopiesController : Controller
    {
        private readonly LabContext _context;

        public BookCopiesController(LabContext context)
        {
            _context = context;
        }

        // GET: BookCopies
        public async Task<IActionResult> Index(string searchString)
        {
            var bookCopies = await _context.BookCopies.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                int i = 0;
                Int32.TryParse(searchString, out i);
                bookCopies = bookCopies.Where(c => c.BookID.Equals(i)).ToList();
            }
            return View(bookCopies);
        }

        // GET: BookCopies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopy = await _context.BookCopies
                .Include(b => b.Book)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            return View(bookCopy);
        }

        // GET: BookCopies/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID");
            return View();
        }

        // POST: BookCopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BookID,DonationDate,AvailableForRental,Language")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCopy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopy = await _context.BookCopies.SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopy == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", bookCopy.BookID);
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BookID,DonationDate,AvailableForRental,Language")] BookCopy bookCopy)
        {
            if (id != bookCopy.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCopy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCopyExists(bookCopy.ID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "ID", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopy = await _context.BookCopies
                .Include(b => b.Book)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCopy = await _context.BookCopies.SingleOrDefaultAsync(m => m.ID == id);
            _context.BookCopies.Remove(bookCopy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCopyExists(int id)
        {
            return _context.BookCopies.Any(e => e.ID == id);
        }
    }
}
