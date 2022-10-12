using Api.Interfaces;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private static readonly List<PedidoViewModel> Pedidos = new()
    {
        new PedidoViewModel
        {
            Produto = "Coca",
            Cliente = "Guilherme"
        },
        new PedidoViewModel
        {
            Produto = "Gelo",
            Cliente = "Guilherme"
        },
        new PedidoViewModel
        {
            Produto = "Coca",
            Cliente = "Pedro"
        },
        new PedidoViewModel
        {
            Produto = "Gelo",
            Cliente = "Pedro"
        },
    };

    private readonly IPedidoService _pedidoService;
    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        await _pedidoService.Processar(Pedidos);
        return Ok();
    }
}
