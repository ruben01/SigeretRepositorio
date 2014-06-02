using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigeret.Models.ViewModels
{
    public class ModeloEquipoItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Modelo { get; set; }
        public bool IsSelected { get; set; }
        public int Cantidad { get; set; }
    }
}