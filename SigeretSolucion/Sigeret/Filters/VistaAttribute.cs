using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigeret.CustomCode
{
    public class VistaAttribute : Attribute 
    {
        public String ViewName { get; set; }
        public String Descriptor { get; set; }

        public VistaAttribute()
        {
            this.ViewName = "";
        }

        public VistaAttribute(String ViewName, String Descriptor)
        {
            this.ViewName = ViewName;
            this.Descriptor = Descriptor;
        }
    }
}