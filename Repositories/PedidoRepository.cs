using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(ApplicationDbContext context) : base(context)
    {
    }
}