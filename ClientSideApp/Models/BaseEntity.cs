using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientSideApp.Models
{
    public class BaseEntity
    {
    //    public DateTime? DateCreated { get; set; }
    //    public DateTime? DateModified { get; set; }
        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}