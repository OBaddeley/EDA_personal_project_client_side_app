using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using ClientSideApp.Models;
using NHandlebars;



namespace ClientSideApp.Controllers
{
    public class CustomersController : ApiController
    {

        private readonly PersonalProjectDbContext _db;

        public CustomersController()
        {
            _db = PersonalProjectDbContext.Create();
        }


        // GET: api/Customers
        public IEnumerable<Customer> Get()
        {

            List<Customer> customers = _db.Customers.ToList();
            return customers;

        }





        // GET: api/Customer/5
        public Customer Get(int id)
        {
            Customer customer = _db.Customers.Find(id);
            return customer;
        }

        // POST: api/Customer
        public void Post([FromBody]Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }


        //// LINK: api/Customer
        //[AcceptVerbs("LINK")]
        //public void PostMany([FromBody]List<Customer> customers)
        //{

        //        customers.ForEach(customer =>
        //        {
        //            customer.ResetId();
        //            _db.Attach(customer, AttachMode.Import);
        //            _db.Add(customer);
        //        });

        //        _db.SaveChanges();

        //}

      
        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _db.Entry(customer).State = EntityState.Modified;

            _db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {

            Customer customer = _db.Customers.Find(id);
            if (customer == null) return;

            _db.Customers.Remove(customer);
            _db.SaveChanges();

        }

        // GET: api/Customers/5/notes
        [Route("api/customers/{customerId}/notes")]
        [AcceptVerbs("GET")]
        public String GetNotesForCustomer(int customerId)
        {

            Customer customer = _db.Customers.Find(customerId);
            if (customer == null) { return String.Empty; }

            String template = "<ul class='notes'>{{#each notes}}<li class='note'>{{this}}</li>{{/each}}</ul>";
            var data = new { createdate = customer.CreatedOn, notes = customer.Notes.OrderByDescending(o => o.CreatedOn).Select(s => s.Text) };
            var output = Handlebars.Render(template, data);

            return output;
        }

        // POST: api/Customers/5/notes
        [Route("api/customers/{customerId}/notes")]
        [AcceptVerbs("POST")]
        public void PostNoteForCustomer(int customerId, [FromBody]String value)
        {

            Customer customer = _db.Customers.Find(customerId);
            if (customer == null) return;

            Note note = new Note() { Text = value, Customer =customer };

            customer.Notes.Add(note);

            _db.SaveChanges();

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
}
