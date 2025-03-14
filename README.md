# Base.Exchange.OrderGenerator.App

## Visão Geral
A aplicação **Base.Exchange.OrderGenerator.App** foi desenvolvida em Blazor com o objetivo de gerar ordens via o protocolo FIX 4.4, utilizando a biblioteca QuickFIX/N. A aplicação se comunica com a aplicação **OrderAccumulator**, escrita em C#, para processar as ordens.

## Funcionalidades
- Interface web para criação de ordens (NewOrderSingle).
- Comunicação via protocolo FIX 4.4.
- Integração com a biblioteca QuickFIX/N.

## Requisitos
- .NET 6.0 ou superior
- Blazor Server
- Biblioteca QuickFIX/N

## Campos do Formulário
O formulário de criação de ordens contém os seguintes campos:

- **Símbolo**: Escolhido entre PETR4, VALE3 ou VIIA4.
- **Lado**: Compra ou Venda.
- **Quantidade**: Valor positivo inteiro menor que 100.000.
- **Preço**: Valor positivo decimal múltiplo de 0.01 e menor que 1.000.

## Como Executar
1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/Base.Exchange.OrderGenerator.App.git
```

2. Acesse a pasta do projeto:

```bash
cd Base.Exchange.OrderGenerator.App
```

3. Restaure as dependências:

```bash
dotnet restore
```

4. Execute a aplicação:

```bash
dotnet run
```

5. Acesse no navegador:

```
http://localhost:5000
```

## Configuração do QuickFIX/N
No arquivo `initiator.cfg`, configure as sessões FIX de acordo com as necessidades da aplicação. Exemplo:

```ini
[DEFAULT]
ConnectionType=initiator
StartTime=00:00:00
EndTime=23:59:59
FileStorePath=store

[SESSION]
BeginString=FIX.4.4
SenderCompID=OrderGenerator
TargetCompID=OrderAccumulator
SocketConnectHost=localhost
SocketConnectPort=5001
```
