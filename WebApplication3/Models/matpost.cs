using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class matpost
    {
        public List<material> materials { get; set; }
        public List<blogg> posts { get; set; }
        public material selectedmaterial { get; set; }
        public blogg selectedpost { get; set; }
    }
}