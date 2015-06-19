using ClientSideApp.Models;
using dotNetExt;
using Faker;

namespace ClientSideApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClientSideApp.Models.PersonalProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClientSideApp.Models.PersonalProjectDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            int customersToGenerate = 30;

            var fake = new Fake<Customer>();


            customersToGenerate.Times(i =>
            {
                String name = Faker.Name.FullName();
                Customer customer = fake.Generate();
                customer.Phone_number = Phone.Number();
                customer.Email = Faker.Internet.Email(name);
                customer.Name = name;
                
                5.Times(k =>
                {
                    var note = new Note();
                    note.Text = Lorem.Paragraph();
                    customer.Notes.Add(note);
                });

                context.Customers.Add(customer);

            });
            context.SaveChanges();
           

        }
    }
}
