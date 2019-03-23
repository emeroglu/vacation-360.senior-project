using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaHotel : Base
    {        
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stars { get; set; }

        public int PhoneNumber { get; set; }
    
        public string Address { get; set; }
       
        public string Information { get; set; }

        public string Email { get; set; }

        public virtual vaCity City { get; set; }

        public virtual vaMember Member { get; set; }

        public virtual List<vaPhoto> Photos { get; set; }
    }
}