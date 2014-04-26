using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class UsuarioNipSmMap : EntityTypeConfiguration<UsuarioNipSm>
    {
        public UsuarioNipSmMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nip)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UsuarioNipSms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nip).HasColumnName("Nip");
            this.Property(t => t.IdUserProfile).HasColumnName("IdUserProfile");

            // Relationships
            this.HasRequired(t => t.UserProfile)
                .WithMany(t => t.UsuarioNipSms)
                .HasForeignKey(d => d.IdUserProfile);

        }
    }
}
