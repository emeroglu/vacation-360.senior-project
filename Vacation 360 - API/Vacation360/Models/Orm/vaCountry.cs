﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaCountry : Base
    {
        public string Name { get; set; }

        public virtual List<vaCity> Cities { get; set; }
    }
}