[English](README.md) | Português

# mcp-dotnet

> **Início rápido**

```bash
dotnet build
dotnet run --project src/PedidosMcpClient --no-build
```

Precisa só do SDK do .NET 10. Detalhes em [Como rodar](#como-rodar).

Um exemplo mínimo de servidor e cliente [MCP (Model Context Protocol)](https://modelcontextprotocol.io) em .NET 10, usando o pacote NuGet oficial `ModelContextProtocol` (2.2.0) sobre o transporte stdio.

## O que é

- **PedidosMcpServer**: um servidor MCP que expõe um repositório de pedidos em memória (4 pedidos de exemplo) como três tools:
  - `obter_pedido` (somente leitura): retorna um pedido pelo id.
  - `listar_pedidos` (somente leitura): lista pedidos, opcionalmente filtrados por status (`Criado`, `Pago`, `Entregue`, `Cancelado`).
  - `cancelar_pedido` (destrutiva): cancela um pedido, exceto se já foi entregue.
- **PedidosMcpClient**: um cliente de console que inicia o servidor como processo filho, lista as tools descobertas e chama `listar_pedidos` com `status = Pago`.

No modo stdio o stdout é o canal do protocolo, por isso o servidor envia seus logs para o stderr.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

Sem dependências externas (sem banco de dados, sem serviços de rede).

## Como rodar

Na raiz do repositório:

```bash
dotnet build
dotnet run --project src/PedidosMcpClient --no-build
```

O cliente inicia o servidor com `dotnet run --project src/PedidosMcpServer --no-build`; por isso o build precisa vir antes e o comando deve ser executado na raiz do repositório.

Saída esperada:

```
Discovered tools / Ferramentas descobertas:
- cancelar_pedido: Cancels an order, unless it was already delivered. / Cancela um pedido, exceto se já foi entregue.
- obter_pedido: Returns an order: customer, total and status. / Retorna um pedido: cliente, total e status.
- listar_pedidos: Lists orders, filtering by status. / Lista pedidos, filtrando pelo status.

listar_pedidos(status: Pago):
[{"id":1,"cliente":"Ana","total":120.50,"status":"Pago"}]
```

(A ordem das tools pode variar.)

## Estrutura

```
mcp-dotnet.slnx
src/
  PedidosMcpServer/   servidor MCP (Program.cs, PedidoTools.cs, PedidoRepositorio.cs, Pedido.cs)
  PedidosMcpClient/   cliente de console (Program.cs)
```

Identificadores, nomes de tools e valores de enum permanecem em português de propósito; descrições e mensagens são bilíngues ("English / Português").
