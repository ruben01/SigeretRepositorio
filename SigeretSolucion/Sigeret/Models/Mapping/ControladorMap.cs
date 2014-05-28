using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class ControladorMap : EntityTypeConfiguration<Controlador>
    {
        public ControladorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descriptor)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Controlador");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Descriptor).HasColumnName("Descriptor");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
