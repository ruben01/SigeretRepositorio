using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class EstatusMatricula
    {
        public EstatusMatricula()
        {
            this.MatriculaUASDs = new List<MatriculaUASD>();
        }

        public int Id { get; set; }
        public string Estatus { get; set; }
        public virtual ICollection<MatriculaUASD> MatriculaUASDs { get; set; }
    }
}
