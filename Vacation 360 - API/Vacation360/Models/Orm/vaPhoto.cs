using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class vaPhoto : Base
    {
        public string Title { get; set; }

        public int Rating { get; set; }

        public int Likes { get; set; }

        public string RawUrl { get; set; }

        public string PanoramicUrl { get; set; }

        public virtual vaHotel Hotel { get; set; }

        public vaPhoto()
        {

        }

        public vaPhoto(string panoramicUrl)
        {
            this.PanoramicUrl = panoramicUrl;
        }
    }
}