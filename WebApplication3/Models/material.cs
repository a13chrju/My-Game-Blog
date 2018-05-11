using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class material
    {
        public string imageurl { get; set; }
        public int type { get; set; }
        public string description { get; set; }
        public int index { get; set; }
        public int postid { get; set; }
        public string BlenderFile { get; set; }
    }
}