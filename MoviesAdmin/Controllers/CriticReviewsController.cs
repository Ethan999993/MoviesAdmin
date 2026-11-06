
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin;


    [Authorize]
    public class CriticReviewsController : Controller
    {
        private readonly MoviesAdminContext _context;
        private string? _userId;

        public CriticReviewsController(MoviesAdminContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _userId = User.GetUserId();
            
        }

   
        // GET: CriticReviews
        public async Task<IActionResult> Index()
        {
        // Get reviews that belong to the logged in user
        var reviews = await _context.CriticReview
            .Where(c => c.CreatedBy == _userId)
            .Include(c => c.Movie)
            .ToListAsync();

        return View(reviews);
    }
        // GET: CriticReviews/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReview
                 .Where(m =>m.CreatedBy == _userId)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (criticReview == null)
            {
                return NotFound();
            }

            return View(criticReview);
        }

        // GET: CriticReviews/Create
        public IActionResult Create(int? movieId)
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "id", "Title", movieId);
            return View();
        }

        // POST: CriticReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,Description,Rating,IsPublished")] CriticReview criticReview)


        {
            criticReview.CreatedBy = _userId;  
              


            if (ModelState.IsValid)
            {
                
                _context.Add(criticReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_context.Movie, "id", "Title", criticReview.MovieId);
            return View(criticReview);
        }

        // GET: CriticReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReview
                .Where(c =>c.CreatedBy == _userId && c.IsPublished == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (criticReview == null)
            {
                return NotFound();
            }

            ViewData["MovieId"] = new SelectList(_context.Movie, "id", "Title", criticReview.MovieId);
            return View(criticReview);
        }

        // POST: CriticReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,Description,Rating,IsPublished,CreatedBy,CreatedDate")] CriticReview criticReview)
        {
            if (id != criticReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criticReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticReviewExists(criticReview.Id))
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

            ViewData["MovieId"] = new SelectList(_context.Movie, "id", "Title", criticReview.MovieId);
            return View(criticReview);
        }

        // GET: CriticReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReview
                .Where(m => m.CreatedBy == _userId)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (criticReview == null)
            {
                return NotFound();
            }

            return View(criticReview);
        }

        // POST: CriticReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criticReview = await _context.CriticReview.FindAsync(id);

            if (criticReview != null)
            {
                _context.CriticReview.Remove(criticReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticReviewExists(int id)
        {
            return _context.CriticReview.Any(e => e.Id == id);
        }
    }


 

