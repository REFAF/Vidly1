using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly1.Models;
using Vidly1.ViewModels;
using System.Data.Entity;
using Vidly1.Migrations;

namespace Vidly1.Controllers
{
    // MoviesController is a simple class that derives from Controller class (5)
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context; //ex3.3

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }//ex3.3
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }//ex3.3

        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie) //modified in ex5 
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }



        public ViewResult Index()
        {
            //var movies = GetMovies(); //ex2
            var movies = _context.Movies.Include(m => m.Genre).ToList(); //ex3.3

            return View(movies);
        }


        //ex3.3
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // ex5
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }


            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now; 
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        //ex2
        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

        // when a request comes in the application ASP.NET MVC automaticly maps request data to parameter values for acrion method (11)
        // if the action method takes parameters the MVC framework looks for parameters which the same in the request data. (11)
        // if the para. exists -> the framework will pass the value of that para. to the target action. (11)
        //Parameters sources: (11)
        // - in the URL: /movie/edit/1
        // - in query string: /movie/edit?id=1
        // - in the form data: id=1

        //public ActionResult Edit (int id)
        //{
        //    return Content("id = " + id);
        //}


        // /movies
        // if the page index not specified we display the movies in page 1, 
        // if the sort by not specified we sort the movies by their name. (11)
        // To make the paras. optional we should make it nullable (11)
        // int? -> nullable int
        // string in C# is refrence type and it is nullable 
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}



        // GET: Movies/Random -> action called Random, return ActionResult (5)
        // this ActionResult is the base class for all action results in ASP.NET MVC. (10)
        // depending on what an action does, it would return an instance of one of the classes that derive from ActionResult. (10)

        // if we set the return type to -> ViewResult (bcs it's return a view), it's good practice. (10)
        // But sometimes an action may have different execution paths and return different action result.in that case we 
        // need to set the return type of that action into ActionResult, so we can return any of it subtypes. (10)

        //GET: Movies/Random
        public ActionResult Random()
        {
            // create an instance of Movie model (1.5)
            var movie = new Movie() { Name = "Shrek!" };
            // (15)
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            // View method : helper method inherited rfom the base class (Controller class) (10)
            // this method allows us to quicly create a view result. (10)
            // this approch more easy and common amoung develovers. (10)

            // We passed our model to the view by passing it as an argument to the view method.(14.1)
            //There are two other ways to pass data to views (14)
            //1- use the view data dictionary.
            //every controller has a property called ViewData which is of type of data dictionary, pass our movie to the view
            //then we remove it from here -> return View(movie) (it be) -> return View().>>> go to Random.cshtml

            //ViewData["Movie"] = movie;

            //return View(movie);
            return View(viewModel);

            // Alternatively, we can create a view result like this: (10)
            //return new ViewResult();


            //return Content("Hello Wold!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
            // first arg: name of the action. (10)
            // second arg: controller. (10)
            // third arg: when we want to pass argument to the target action, we use anonymous object. (10)
        }


        // aplply route attribute (13.2) 
        //[Route("url templet") to apply constraint -> :regex(like a function)(\\d{2})
        // with attribute route we can aply other kind of constraints -> :regex(first):range(second).
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]

        //public ActionResult ByReleaseDate(int year, int month) //(11.1)
        //{
        //    return Content(year + "/" + month);
        //    // then go to RouteConfig.cs
        //}
    }
}