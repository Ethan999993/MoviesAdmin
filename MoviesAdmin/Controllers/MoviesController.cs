
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;
using System.ComponentModel.DataAnnotations;

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;

    private readonly AzureStorageService _azureStorage;

    public MoviesController(MoviesAdminContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: MOVIES/Index
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movie
            .OrderByDescending(t => t.ReleaseDate)// order by date descending 
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .ToListAsync(); // get all movies from db 

        return View(movies); // display  in view 
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        // is a Id included in URL
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.id == id);  // get movie from db where is = 5

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

        // create a select list for the categories
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        // create a select list for the categories
        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");



        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,Title,Synopsis,RuntimeMinutes,ReleaseDate,GenreId,AudienceRatingId")] Movie movie, IFormFile? ImageUpload)
    {
        //Validate if image file is MissingFieldException 
        if (ImageUpload == null)
        {
            //Include a validation error message and return form
            ModelState.AddModelError("ImageUpload", "Image file is required");

        }
        if (ModelState.IsValid) // validate input 
        {

            //make a unique filename (GUID.ext)
            string extension = Path.GetExtension(ImageUpload!.FileName); // includes the leading dot, e.g. ".jpg"
            string newFileName = $"{Guid.NewGuid()}{extension}";


            // upload file to azure storage 
            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);

            // set the image url in movie record
            movie.ImageUrl = url;

            _context.Add(movie); // add new movue to context (db)
            await _context.SaveChangesAsync(); // save context changes in db 

            return RedirectToAction(nameof(Index)); // re driect to Movies/Index page
        }


        // create a select list for the categories
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");
        // create a select list for the categories
        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");



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

        // create a select list for the categories
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        // create a select list for the categories
        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");


        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,Title,Synopsis,RuntimeMinutes,ReleaseDate,ImageUrl,GenreId,AudienceRatingId")] Movie movie)
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

        // create a select list for the categories
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        // create a select list for the categories
        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");


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
