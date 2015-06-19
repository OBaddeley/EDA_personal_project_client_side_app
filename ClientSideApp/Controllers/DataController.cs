using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientSideApp.Models;
using dotNetExt;
using Faker;

namespace ClientSideApp.Controllers
{

#if DEBUG
    public class DataController : Controller
    {

        private readonly PersonalProjectDbContext _db;

        public DataController()
        {
            _db = PersonalProjectDbContext.Create();
        }


        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purge()
        {

            foreach (var customer in _db.Customers)
            {
                _db.Customers.Remove(customer);
            }

            _db.SaveChanges();
            return View();

        }
        public ActionResult Generate(int customersToGenerate = 30)
        {

            var fake = new Fake<Customer>();


            customersToGenerate.Times(i =>
            {
                Customer customer = fake.Generate();
                customer.Phone_number = Phone.Number();
                5.Times(k =>
                {
                    var note = new Note();
                    note.Text = Lorem.Paragraph();
                    customer.Notes.Add(note);
                });

                _db.Customers.Add(customer);

            });
            _db.SaveChanges();
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
#endif
}