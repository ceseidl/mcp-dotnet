using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PedidosMcpServer;

var builder = Host.CreateApplicationBuilder(args);

// No stdio, o stdout é o canal do protocolo: os logs vão
// para stderr, senão corrompem as mensagens JSON-RPC.
builder.Logging.AddConsole(o =>
    o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddSingleton<PedidoRepositorio>();
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
