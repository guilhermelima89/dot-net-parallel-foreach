using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
