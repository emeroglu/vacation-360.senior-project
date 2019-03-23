using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaPhotoTag : Base
    {
        public virtual vaPhoto Photo { get; set; }

        public virtual vaTag Tag { get; set; }
    }
}