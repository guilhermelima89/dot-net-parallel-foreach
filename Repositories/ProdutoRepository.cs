using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context)
    {
    }
}