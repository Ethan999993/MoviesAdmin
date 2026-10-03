
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using System.ComponentModel.DataAnnotations;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;

    public MoviesController(MoviesAdminContext context)
    {
        _context = context;
    }

    // GET: MOVIES/Index
    public async Task<IActionResult> Index()    
    {
        var movies = await _context.Movie
            .OrderByDescending(t => t.ReleaseDate)// order by date descending 
            .ToListAsync(); // get all movies from db 
        
        return View(movies); // display trails in view 
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        // is a Id included in URL
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FirstOrDefaultAsync(m => m.id = id);  // get movie from db where is = 5

        // was the row found in the database?
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,Title,Synopsis,Genre,Rating,RuntimeMinutes,ReleaseDate")] Movie movie)
    {
        if (ModelState.IsValid) // validate input 
        {
            _context.Add(movie); // add new movue to context (db)
            await _context.SaveChangesAsync(); // save context changes in db 
           
            return RedirectToAction(nameof(Index)); // re driect to Movies/Index page
        }
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,Title,Synopsis,Genre,Rating,RuntimeMinutes,ReleaseDate")] Movie movie)
    {
        if (id != movie.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.id))
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
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.id == id);
    }
}
