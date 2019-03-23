using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models
{
    public static class CurrentUser
    {
        public static int ID
        {
            get
            {
                string Id = HttpContext.Current.User.Identity.Name.Split(';')[0];
                return int.Parse(Id);
            }
        }

        public static string Email
        {
            get
            {
                return HttpContext.Current.User.Identity.Name.Split(';')[2];
            }
        }
    }
}