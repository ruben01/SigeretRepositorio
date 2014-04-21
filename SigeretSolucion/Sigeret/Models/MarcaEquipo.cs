using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class MarcaEquipo
    {
        public MarcaEquipo()
        {
            this.ModeloEquipoes = new List<ModeloEquipo>();
        }

        public int Id { get; set; }
        public string Marca { get; set; }
        public virtual ICollection<ModeloEquipo> ModeloEquipoes { get; set; }
    }
}
