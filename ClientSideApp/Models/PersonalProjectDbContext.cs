namespace ClientSideApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PersonalProjectDbContext : DbContext
    {
        // Your context has been configured to use a 'PersonalProjectDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ClientSideApp.Models.PersonalProjectDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PersonalProjectDbContext' 
        // connection string in the application configuration file.
        public PersonalProjectDbContext()
            : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(true);
        }

        public static PersonalProjectDbContext Create()
        {
            return new PersonalProjectDbContext();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //var currentUsername = HttpContext.Current != null && HttpContext.Current.User != null
            //    ? HttpContext.Current.User.Identity.Name
            //    : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedOn = DateTime.UtcNow;
                    //((BaseEntity)entity.Entity).UserCreated = currentUsername;
                }

                ((BaseEntity)entity.Entity).UpdatedOn = DateTime.UtcNow;
                //((BaseEntity)entity.Entity).UserModified = currentUsername;
            }

            return base.SaveChanges();
        }

    }

    
}