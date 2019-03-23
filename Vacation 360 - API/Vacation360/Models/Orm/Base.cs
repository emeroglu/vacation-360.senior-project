using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class Base
    {
        [Key]
        public int ID { get; set; }
    }
}