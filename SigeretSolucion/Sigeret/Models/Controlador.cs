using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Controlador
    {
        public Controlador()
        {
            this.Accions = new List<Accion>();
        }

        public int Id { get; set; }
        public string Descriptor { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Accion> Accions { get; set; }
    }
}
