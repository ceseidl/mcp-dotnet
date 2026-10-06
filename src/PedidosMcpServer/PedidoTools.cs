using System.ComponentModel;
using ModelContextProtocol.Server;

namespace PedidosMcpServer;

[McpServerToolType]
public class PedidoTools(PedidoRepositorio repositorio)
{
    [McpServerTool(Name = "obter_pedido", ReadOnly = true)]
    [Description("Retorna um pedido: cliente, total e status.")]
    public Pedido? ObterPedido(
        [Description("Id numérico do pedido")] int id) =>
        repositorio.Obter(id);

    [McpServerTool(Name = "listar_pedidos", ReadOnly = true)]
    [Description("Lista pedidos, filtrando pelo status.")]
    public IReadOnlyList<Pedido> ListarPedidos(
        [Description("Criado, Pago, Entregue ou Cancelado")]
        StatusPedido? status = null) =>
        repositorio.Listar(status);

    [McpServerTool(Name = "cancelar_pedido", Destructive = true)]
    [Description("Cancela um pedido, exceto se já foi entregue.")]
    public string CancelarPedido(
        [Description("Id numérico do pedido")] int id) =>
        repositorio.Cancelar(id)
            ? $"Pedido {id} cancelado."
            : $"Pedido {id} não encontrado ou já entregue.";
}
