using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaComment : Base
    {
        public string Text { get; set; }

        public virtual vaPhoto Photo { get; set; }
    }
}