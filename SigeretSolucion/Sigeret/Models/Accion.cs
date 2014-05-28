using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Accion
    {
        public Accion()
        {
            this.webpages_Roles = new List<webpages_Roles>();
        }

        public int Id { get; set; }
        public int ControladorId { get; set; }
        public string Name { get; set; }
        public string Descriptor { get; set; }
        public virtual Controlador Controlador { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
