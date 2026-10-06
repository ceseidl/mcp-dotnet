namespace PedidosMcpServer;

public class PedidoRepositorio
{
    private readonly List<Pedido> _pedidos =
    [
        new(1, "Ana", 120.50m, StatusPedido.Pago),
        new(2, "Bruno", 89.90m, StatusPedido.Criado),
        new(3, "Carla", 240.00m, StatusPedido.Entregue),
        new(4, "Diego", 59.00m, StatusPedido.Cancelado),
    ];

    public Pedido? Obter(int id) =>
        _pedidos.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<Pedido> Listar(StatusPedido? status) =>
        _pedidos
            .Where(p => status is null || p.Status == status)
            .ToList();

    public bool Cancelar(int id)
    {
        var i = _pedidos.FindIndex(p => p.Id == id);
        if (i < 0 || _pedidos[i].Status is StatusPedido.Entregue)
            return false;

        _pedidos[i] = _pedidos[i] with
        {
            Status = StatusPedido.Cancelado
        };
        return true;
    }
}
