using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DownloadMaterials
    {
        public List<material> materials { get; set; }
        public material selectedmaterial { get; set;  }
    }
}
