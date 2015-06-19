using Newtonsoft.Json;

namespace ClientSideApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Notes = new HashSet<Note>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone_number { get; set; }

        [StringLength(300)]
        public string Email { get; set; }


        [JsonIgnore]
        public virtual ICollection<Note> Notes { get; set; }


       
        public DateTime? created_at
        {
            get { return this.CreatedOn; }
        }

        
        public DateTime? updated_at
        {
            get { return this.UpdatedOn; }
        }
    }
}
