using Api.Interfaces;
using Api.Models;
using Api.ViewModels;

namespace Api.Services;

public class PedidoService : IPedidoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPedidoRepository _pedidoRepository;
    public PedidoService(
        IProdutoRepository produtoRepository,
        IClienteRepository clienteRepository,
        IPedidoRepository pedidoRepository
        )
    {
        _produtoRepository = produtoRepository;
        _clienteRepository = clienteRepository;
        _pedidoRepository = pedidoRepository;
    }

    public void Processar(List<PedidoViewModel> pedidos)
    {
        var obLock = new Object();
        var _pedidos = new List<Pedido>();

        Parallel.ForEach(pedidos, (PedidoViewModel pedido) =>
        {
            int produtoId;
            int clienteId;

            // Pesquisar & Adicionar Produto
            lock (obLock)
            {
                var produto = _produtoRepository.Where(x => x.Descricao == pedido.Produto);

                if (produto is null)
                {
                    var novoProduto = new Produto { Descricao = pedido.Produto };
                    _produtoRepository.Add(novoProduto);
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
                var cliente = _clienteRepository.Where(x => x.Nome == pedido.Cliente);

                if (cliente is null)
                {
                    var novoCliente = new Cliente { Nome = pedido.Cliente };
                    _clienteRepository.Add(novoCliente);
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

        _pedidoRepository.AddRange(_pedidos);
        // _context.Pedido.AddRange(_pedidos);
        // await _context.SaveChangesAsync();
    }
}
