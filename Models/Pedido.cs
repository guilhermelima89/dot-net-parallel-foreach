namespace Api.Models;

public class Pedido : Entity
{
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
}