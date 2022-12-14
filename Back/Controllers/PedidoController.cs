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
        new PedidoViewModel
        {
            Produto = "Coca",
            Cliente = "Jessica"
        },
        new PedidoViewModel
        {
            Produto = "Gelo",
            Cliente = "Jessica"
        },
    };

    private readonly IPedidoService _pedidoService;
    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public IActionResult Post()
    {
        _pedidoService.Processar(Pedidos);
        return Ok();
    }
}
