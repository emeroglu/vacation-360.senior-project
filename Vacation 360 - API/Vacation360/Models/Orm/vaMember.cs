using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaMember : Base
    {
       
        [Display(Name="Name")]
        public string Name { get; set; }

        
        [Display(Name="Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Email can not be blank")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name="Email")]
        public string Email { get; set; }

        
        [Display(Name="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password can not be blank")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name="Last Seen")]
        public DateTime LastSeen { get; set; }

        public bool Online { get; set; }

        public virtual vaMemberType Type { get; set; }
    }
}