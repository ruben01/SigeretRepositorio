using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigeret.CustomCode
{
    public class EsControllerAttribute : Attribute
    {
        public String ControllerName { get; set; }
        public String Descriptor { get; set; }

        public EsControllerAttribute()
        {
            this.ControllerName = "";
        }

        public EsControllerAttribute(String ControllerName, String Descriptor)
        {
            this.ControllerName = ControllerName;
            this.Descriptor = Descriptor;
        }
    }
}