using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class AulaEdificioMap : EntityTypeConfiguration<AulaEdificio>
    {
        public AulaEdificioMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Aula)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AulaEdificio");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Aula).HasColumnName("Aula");
            this.Property(t => t.IdLugar).HasColumnName("IdLugar");

            // Relationships
            this.HasRequired(t => t.Lugar)
                .WithMany(t => t.AulaEdificios)
                .HasForeignKey(d => d.IdLugar);

        }
    }
}
