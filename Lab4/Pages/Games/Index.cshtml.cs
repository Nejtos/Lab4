#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab4.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly Lab4.Data.Lab4Context _context;

        public IndexModel(Lab4.Data.Lab4Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GameGenre { get; set; }
        public string NameSort { get; set; }
        public IList<Game> Games { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;

            IQueryable<string> genreQuery = from m in _context.Game
                                            orderby m.Genre
                                            select m.Genre;
            var games = from m in _context.Game select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(GameGenre))
            {
                games = games.Where(x => x.Genre == GameGenre);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(s => Convert.ToInt32(s.Rating));
                    break;
                default:
                    games = games.OrderBy(s => Convert.ToInt32(s.Rating));
                    break;
            }
            Games = await games.ToListAsync();
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Games = await games.AsNoTracking().ToListAsync();
        }
    }
}