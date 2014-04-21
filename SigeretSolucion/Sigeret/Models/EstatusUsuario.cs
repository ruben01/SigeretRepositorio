using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class EstatusUsuario
    {
        public EstatusUsuario()
        {
            this.UserProfiles = new List<UserProfile>();
        }

        public int Id { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
