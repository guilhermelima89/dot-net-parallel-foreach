using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProdutoId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.ClienteId)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(x => x.Produto);

            builder.HasOne(x => x.Cliente);

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.ToTable("Pedido");
        }
    }
}
