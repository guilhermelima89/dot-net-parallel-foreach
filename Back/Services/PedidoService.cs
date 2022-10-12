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
        var obLock = new object();
        var listaPedidos = new List<Pedido>();

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
                    // Adicionar novo produto
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
                    // Adicionar novo cliente
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

            listaPedidos.Add(novoPedido);

        });

        // Salvar Pedido sem duplicar dados
        _pedidoRepository.AddRange(listaPedidos);
    }
}
