using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Data;
using ScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;


namespace ScriptureJournal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Data.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Data.ScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> ScriptureList { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Book { get; set; }
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public async Task OnGetAsync()
        {
            Debug.WriteLine($"book: '{Book}' searchstring: {SearchString}");
            // Use LINQ to get list of books.
            IQueryable<string> genreQuery = from m in _context.Scripture
                                            orderby m.Book
                                            select m.Book;

            var scriptures = from m in _context.Scripture
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Note.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Book))
            {
                scriptures = scriptures.Where(x => x.Book == Book);
            }

            if (!string.IsNullOrEmpty(SortBy))
            {
                if (SortBy == "book")
                {
                    scriptures = scriptures.OrderBy(s => s.Book);
                } else
                {
                    scriptures = scriptures.OrderBy(s => s.DateAdded);
                }
            }
            Books = new SelectList(await genreQuery.Distinct().ToListAsync());
            ScriptureList = await scriptures.ToListAsync();
        }
    }
}
