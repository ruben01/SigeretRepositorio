using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class Lugar
    {
        public Lugar()
        {
            this.AulaEdificios = new List<AulaEdificio>();
        }

        public int Id { get; set; }
        public string Edificio { get; set; }
        public virtual ICollection<AulaEdificio> AulaEdificios { get; set; }
    }
}
