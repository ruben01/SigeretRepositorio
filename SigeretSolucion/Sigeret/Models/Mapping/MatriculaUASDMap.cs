using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sigeret.Models.Mapping
{
    public class MatriculaUASDMap : EntityTypeConfiguration<MatriculaUASD>
    {
        public MatriculaUASDMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Matricula)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MatriculaUASD");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Matricula).HasColumnName("Matricula");
            this.Property(t => t.IdCedula).HasColumnName("IdCedula");
            this.Property(t => t.Estatus).HasColumnName("Estatus");

            // Relationships
            this.HasRequired(t => t.CedulaUASD)
                .WithMany(t => t.MatriculaUASDs)
                .HasForeignKey(d => d.IdCedula);

        }
    }
}
