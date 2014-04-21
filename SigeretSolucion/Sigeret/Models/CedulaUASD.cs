using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class CedulaUASD
    {
        public CedulaUASD()
        {
            this.MatriculaUASDs = new List<MatriculaUASD>();
        }

        public int Id { get; set; }
        public string Cedula { get; set; }
        public virtual ICollection<MatriculaUASD> MatriculaUASDs { get; set; }
    }
}
