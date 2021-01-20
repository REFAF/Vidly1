using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly1.Models;
using Vidly1.ViewModels;
using System.Data.Entity;

namespace Vidly1.Controllers
{
    public class CustomersController : Controller
    {

        //29  we need a DB context to access the database.
        private ApplicationDbContext _context;

        //29  initialize _context in a constructor
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //29
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //38   create a form to add a customer first we need an action.This action should return a view that includes the form.
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }

        [HttpPost]//42
        [ValidateAntiForgeryToken]//57
        public ActionResult Save(Customer customer )
        {

            //50   MVC also uses data annotations to validate actual parameters.So in customer's controller in the save action 
            //here we have customer as a parameter when ASP.NET MVC populates this customer object using requests data 
            //it checks to see if this object is valid based on the data annotations applied on various properties of this customer class.
            //Now at this point in the controller we can use ModelState property to get access to validation data

            //50   to add validation there are three steps you need to follow.
            //The first step is to add data annotations on your entities.
            //The second step is to use model state that valid to change the flow of the program.And if the model state is not valid you return the same view.
            //The third step add validation messages to our form.(Add placeholder for validation messages next to each field that requires validation.

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer, //set customer property of CustomerFormViewModel to this customer object we receive in this action.
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                //return ta same view
                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewaLetter = customer.IsSubscribedToNewaLetter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customer 
        public ViewResult Index()
        {
            //var customers = GetCustomers();  //ex2
            //var customers = _context.Customers.ToList(); // 29
            //var customers = _context.Customers.Include(c=> c.MembershipType).ToList(); //30 Include need namespace System.Data.Entity.

            return View();
        }

        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);   //ex2
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);  //29, include in ex 3.2

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        //43
        public ActionResult Edit(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);

        }
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //} //ex2  -> in 29 we delete it

    }

}