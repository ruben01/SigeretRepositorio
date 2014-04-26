using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class CedulaUASDMap : EntityTypeConfiguration<CedulaUASD>
    {
        public CedulaUASDMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Cedula)
                .IsRequired()
                .HasMaxLength(11);

            // Table & Column Mappings
            this.ToTable("CedulaUASD");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Cedula).HasColumnName("Cedula");
        }
    }
}
