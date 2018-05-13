using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class blogg
    {
        public string video_url { get; set; }
        public string titel { get; set; }
        public string text { get; set; }
        public string datum { get; set; }
        public int index { get; set; }
        public int category { get; set; }
        public int episode { get; set; }
        public string thumbnail { get; set; }
        public List<material> materials { get; set; }
    }
}