using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class MatriculaUASD
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public int IdCedula { get; set; }
        public int Estatus { get; set; }
        public virtual CedulaUASD CedulaUASD { get; set; }
    }
}
