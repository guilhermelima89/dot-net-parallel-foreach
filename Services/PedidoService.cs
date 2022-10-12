using System.Collections.Concurrent;
using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PedidoService : IPedidoService
{
    private readonly ApplicationDbContext _context;
    public PedidoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Processar(List<PedidoViewModel> pedidos)
    {
        var obLock = new Object();
        var _pedidos = new ConcurrentBag<Pedido>();

        Parallel.ForEach(pedidos, (PedidoViewModel pedido) =>
        {
            int produtoId;
            int clienteId;

            // Pesquisar & Adicionar Produto
            lock (obLock)
            {
                var produto = _context.Produto.FirstOrDefaultAsync(x => x.Descricao == pedido.Produto);

                if (produto is null)
                {
                    var novoProduto = new Produto { Descricao = pedido.Produto };
                    _context.Produto.Add(novoProduto);
                    _context.SaveChangesAsync();
                    produtoId = novoProduto.Id;
                }
                else
                {
                    produtoId = produto.Id;
                }
            }

            // Pesquisar & Adicionar Cliente
            lock (obLock)
            {
                var cliente = _context.Cliente.FirstOrDefaultAsync(x => x.Nome == pedido.Cliente);

                if (cliente is null)
                {
                    var novoCliente = new Cliente { Nome = pedido.Cliente };
                    _context.Cliente.Add(novoCliente);
                    _context.SaveChangesAsync();
                    clienteId = novoCliente.Id;
                }
                else
                {
                    clienteId = cliente.Id;
                }
            }

            var novoPedido = new Pedido
            {
                ClienteId = clienteId,
                ProdutoId = produtoId
            };

            _pedidos.Add(novoPedido);

        });

        _context.Pedido.AddRange(_pedidos);
        await _context.SaveChangesAsync();
    }
}