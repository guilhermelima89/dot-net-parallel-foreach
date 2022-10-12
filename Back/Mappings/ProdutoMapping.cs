using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("varchar(250)");

        /* ENTITY */
        builder.Property(x => x.DataCadastro)
            .HasColumnType("datetime")
            .HasDefaultValueSql("getdate()");

        builder.ToTable("Produto");
    }
}
