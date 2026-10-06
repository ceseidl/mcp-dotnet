using System.ComponentModel;
using ModelContextProtocol.Server;

namespace PedidosMcpServer;

[McpServerToolType]
public class PedidoTools(PedidoRepositorio repositorio)
{
    [McpServerTool(Name = "obter_pedido", ReadOnly = true)]
    [Description("Returns an order: customer, total and status. / Retorna um pedido: cliente, total e status.")]
    public Pedido? ObterPedido(
        [Description("Numeric order id / Id numérico do pedido")] int id) =>
        repositorio.Obter(id);

    [McpServerTool(Name = "listar_pedidos", ReadOnly = true)]
    [Description("Lists orders, filtering by status. / Lista pedidos, filtrando pelo status.")]
    public IReadOnlyList<Pedido> ListarPedidos(
        [Description("Criado, Pago, Entregue or Cancelado / Criado, Pago, Entregue ou Cancelado")]
        StatusPedido? status = null) =>
        repositorio.Listar(status);

    [McpServerTool(Name = "cancelar_pedido", Destructive = true)]
    [Description("Cancels an order, unless it was already delivered. / Cancela um pedido, exceto se já foi entregue.")]
    public string CancelarPedido(
        [Description("Numeric order id / Id numérico do pedido")] int id) =>
        repositorio.Cancelar(id)
            ? $"Order {id} cancelled. / Pedido {id} cancelado."
            : $"Order {id} not found or already delivered. / Pedido {id} não encontrado ou já entregue.";
}
