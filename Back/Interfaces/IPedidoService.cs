using Api.ViewModels;

namespace Api.Interfaces;

public interface IPedidoService
{
    void Processar(List<PedidoViewModel> pedidos);
}
