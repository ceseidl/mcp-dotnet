namespace PedidosMcpServer;

public enum StatusPedido { Criado, Pago, Entregue, Cancelado }

public record Pedido(
    int Id,
    string Cliente,
    decimal Total,
    StatusPedido Status);
