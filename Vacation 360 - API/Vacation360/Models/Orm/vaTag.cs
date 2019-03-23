using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaTag : Base
    {
        [Display(Name="Tag")]
        public string Name { get; set; }
    }
}