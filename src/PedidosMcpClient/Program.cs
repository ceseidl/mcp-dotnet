using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

var opcoes = new StdioClientTransportOptions
{
    Name = "pedidos",
    Command = "dotnet",
    Arguments =
    [
        "run", "--project", "src/PedidosMcpServer", "--no-build"
    ],
};

await using var cliente =
    await McpClient.CreateAsync(new StdioClientTransport(opcoes));

Console.WriteLine("Discovered tools / Ferramentas descobertas:");
foreach (var tool in await cliente.ListToolsAsync())
    Console.WriteLine($"- {tool.Name}: {tool.Description}");

var resultado = await cliente.CallToolAsync(
    "listar_pedidos",
    new Dictionary<string, object?> { ["status"] = "Pago" });

Console.WriteLine();
Console.WriteLine("listar_pedidos(status: Pago):");
foreach (var bloco in resultado.Content)
    if (bloco is TextContentBlock texto)
        Console.WriteLine(texto.Text);
