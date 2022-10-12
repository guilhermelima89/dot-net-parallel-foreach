using Api.ViewModels;

namespace Api.Interfaces;

public interface IPedidoService
{
    Task Processar(List<PedidoViewModel> pedidos);
}