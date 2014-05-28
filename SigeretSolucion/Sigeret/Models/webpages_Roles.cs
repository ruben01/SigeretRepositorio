using System;
using System.Collections.Generic;

namespace Sigeret.Models
{
    public partial class webpages_Roles
    {
        public webpages_Roles()
        {
            this.Accions = new List<Accion>();
            this.UserProfiles = new List<UserProfile>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Accion> Accions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
