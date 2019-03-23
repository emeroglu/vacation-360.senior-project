using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaFavorite : Base
    {
        public virtual vaPhoto Photo { get; set; }

        public virtual vaMember Member { get; set; }
    }
}