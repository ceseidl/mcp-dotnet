English | [Português](README.pt-BR.md)

[![CI](https://github.com/ceseidl/mcp-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/ceseidl/mcp-dotnet/actions/workflows/ci.yml) [![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

# mcp-dotnet

> **Quick start**

```bash
dotnet build
dotnet run --project src/PedidosMcpClient --no-build
```

Needs only the .NET 10 SDK. Details in [How to run](#how-to-run).

A minimal example of an [MCP (Model Context Protocol)](https://modelcontextprotocol.io) server and client in .NET 10, using the official `ModelContextProtocol` NuGet package (2.2.0) over the stdio transport.

## What it is

- **PedidosMcpServer**: an MCP server that exposes an in-memory order repository (4 sample orders) as three tools:
  - `obter_pedido` (read-only): returns an order by id.
  - `listar_pedidos` (read-only): lists orders, optionally filtered by status (`Criado`, `Pago`, `Entregue`, `Cancelado`).
  - `cancelar_pedido` (destructive): cancels an order, unless it was already delivered.
- **PedidosMcpClient**: a console client that starts the server as a child process, lists the discovered tools and calls `listar_pedidos` with `status = Pago`.

In stdio mode stdout is the protocol channel, so the server sends its logs to stderr.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

No external dependencies (no database, no network services).

## How to run

From the repository root:

```bash
dotnet build
dotnet run --project src/PedidosMcpClient --no-build
```

The client launches the server with `dotnet run --project src/PedidosMcpServer --no-build`, which is why the build must come first and the command must run from the repository root.

Expected output:

```
Discovered tools / Ferramentas descobertas:
- cancelar_pedido: Cancels an order, unless it was already delivered. / Cancela um pedido, exceto se já foi entregue.
- obter_pedido: Returns an order: customer, total and status. / Retorna um pedido: cliente, total e status.
- listar_pedidos: Lists orders, filtering by status. / Lista pedidos, filtrando pelo status.

listar_pedidos(status: Pago):
[{"id":1,"cliente":"Ana","total":120.50,"status":"Pago"}]
```

(The order of the tools may vary.)

## Structure

```
mcp-dotnet.slnx
src/
  PedidosMcpServer/   MCP server (Program.cs, PedidoTools.cs, PedidoRepositorio.cs, Pedido.cs)
  PedidosMcpClient/   console client (Program.cs)
```

Identifiers, tool names and enum values are kept in Portuguese on purpose; descriptions and messages are bilingual ("English / Português").

## License

[MIT](LICENSE). Author: Carlos Eduardo Seidl.
