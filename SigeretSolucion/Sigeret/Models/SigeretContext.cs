using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Sigeret.Models.Mapping;

namespace Sigeret.Models
{
    public partial class SigeretContext : DbContext
    {
        static SigeretContext()
        {
            Database.SetInitializer<SigeretContext>(null);
        }

        public SigeretContext()
            : base("Name=SigeretContext")
        {
        }

        public DbSet<Accion> Accions { get; set; }
        public DbSet<AulaEdificio> AulaEdificios { get; set; }
        public DbSet<CedulaUASD> CedulaUASDs { get; set; }
        public DbSet<Contacto> Contactoes { get; set; }
        public DbSet<Controlador> Controladors { get; set; }
        public DbSet<Equipo> Equipoes { get; set; }
        public DbSet<Lugar> Lugars { get; set; }
        public DbSet<MatriculaUASD> MatriculaUASDs { get; set; }
        public DbSet<ModeloEquipo> ModeloEquipoes { get; set; }
        public DbSet<Solicitud> Solicituds { get; set; }
        public DbSet<TipoContacto> TipoContactoes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<User> Users { get; set; }
       // public DbSet<UsuarioNipSm> UsuarioNipSms { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccionMap());
            modelBuilder.Configurations.Add(new AulaEdificioMap());
            modelBuilder.Configurations.Add(new CedulaUASDMap());
            modelBuilder.Configurations.Add(new ContactoMap());
            modelBuilder.Configurations.Add(new ControladorMap());
            modelBuilder.Configurations.Add(new EquipoMap());
            modelBuilder.Configurations.Add(new LugarMap());
            modelBuilder.Configurations.Add(new MatriculaUASDMap());
            modelBuilder.Configurations.Add(new ModeloEquipoMap());
            modelBuilder.Configurations.Add(new SolicitudMap());
            modelBuilder.Configurations.Add(new TipoContactoMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserMap());
           // modelBuilder.Configurations.Add(new UsuarioNipSmMap());
            modelBuilder.Configurations.Add(new webpages_MembershipMap());
            modelBuilder.Configurations.Add(new webpages_OAuthMembershipMap());
            modelBuilder.Configurations.Add(new webpages_RolesMap());
        }
    }
}
