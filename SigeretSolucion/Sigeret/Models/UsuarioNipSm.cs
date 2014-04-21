using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class UsuarioNipSm
    {
        public int Id { get; set; }
        public string Nip { get; set; }
        public int IdUserProfile { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
