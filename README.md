# OrderFlow API

## Visão Geral

O **OrderFlow API** é um sistema de backend robusto e escalável projetado para gerenciar o fluxo de pedidos de uma plataforma de e-commerce. O projeto resolve o desafio de centralizar e organizar a lógica de negócio relacionada a clientes, produtos e pedidos em uma arquitetura limpa, manutenível e testável.

Construído como um monolito bem estruturado, o OrderFlow serve como uma base sólida que pode evoluir para uma arquitetura de microsserviços, se necessário, demonstrando a aplicação de padrões de design modernos em um ambiente .NET.

## Stack de Tecnologias

As principais tecnologias e frameworks utilizados na construção do projeto são:

| Categoria | Tecnologia | Descrição |
| :--- | :--- | :--- |
| **Framework** | C# / .NET 8 | Plataforma de desenvolvimento principal, moderna e de alta performance. |
| **Web API** | ASP.NET Core Minimal APIs | Para a criação de endpoints HTTP leves, performáticos e com baixo *boilerplate*. |
| **ORM** | Entity Framework Core | Mapeador objeto-relacional para abstrair e gerenciar o acesso ao banco de dados. |
| **Mediador** | MediatR | Implementação do padrão Mediator para orquestrar os fluxos de CQRS de forma desacoplada. |
| **Testes** | xUnit | Framework de testes unitários para garantir a qualidade e a correção do código. |

## Arquitetura

O projeto adota a **Clean Architecture**, que promove uma estrita separação de responsabilidades em camadas concêntricas. Esta abordagem garante baixo acoplamento, alta coesão e independência de frameworks, banco de dados e UI.

> A regra fundamental da Clean Architecture é a **Regra de Dependência**: o código fonte só pode apontar para dentro. Nada em um círculo interno pode saber sobre algo em um círculo externo.

A estrutura é organizada nas seguintes camadas:

-   **Domain**: O núcleo do sistema. Contém as entidades de negócio (`Cliente`, `Produto`, `Pedido`), *value objects* e as regras de negócio mais críticas e puras. Esta camada não possui dependências de nenhuma outra camada do projeto.

-   **Application**: Contém a lógica da aplicação e os casos de uso. Orquestra o fluxo de dados entre o domínio e as camadas externas. É aqui que o padrão **CQRS (Command Query Responsibility Segregation)** é aplicado, separando as operações de escrita (Commands) das operações de leitura (Queries). Define interfaces para as dependências externas (ex: repositórios, serviços de e-mail), mas não suas implementações.

-   **Infrastructure**: Implementa as abstrações definidas na camada de `Application`. Contém detalhes técnicos como a configuração do banco de dados com Entity Framework Core, a implementação dos repositórios e a integração com serviços de terceiros.

-   **Presentation (API)**: A porta de entrada da aplicação. Expõe os casos de uso através de endpoints RESTful utilizando Minimal APIs. É responsável por receber as requisições HTTP, encaminhá-las para a camada `Application` e retornar as respostas formatadas.

A aplicação rigorosa dos princípios **SOLID** em todas as camadas garante que o código seja manutenível, extensível e fácil de testar.

## Funcionalidades

As funcionalidades essenciais implementadas na API incluem:

-   **Gestão de Clientes**: Operações de CRUD (Criar, Ler, Atualizar, Deletar) para clientes.
-   **Gestão de Produtos**: Operações de CRUD (Criar, Ler, Atualizar, Deletar) para produtos.
-   **Gestão de Pedidos**: Criação de novos pedidos associados a um cliente e consulta de pedidos existentes por ID.

## Como Começar (Getting Started)

### Pré-requisitos

Para compilar e executar este projeto localmente, você precisará ter o seguinte software instalado:
*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior.
*   Um editor de código de sua preferência (ex: Visual Studio, VS Code).
*   Git para clonar o repositório.

### Instalação e Execução

Siga os passos abaixo para configurar o ambiente de desenvolvimento:

1.  **Clone o repositório:**
    ```sh
    git clone https://github.com/seu-usuario/orderflow-api.git
    cd orderflow-api
    ```

2.  **Restaure as dependências do projeto:**
    Navegue até o diretório raiz da solução e execute o comando `dotnet restore`.
    ```sh
    dotnet restore
    ```

3.  **Configure o banco de dados:**
    O projeto está configurado para usar o SQL Server LocalDB. As migrações do Entity Framework Core criarão o banco de dados e as tabelas automaticamente. Execute os seguintes comandos a partir do diretório `src/OrderFlow.Api`:
    ```sh
    dotnet ef database update
    ```

4.  **Execute o projeto:**
    Ainda no diretório `src/OrderFlow.Api`, execute o seguinte comando para iniciar a aplicação:
    ```sh
    dotnet run
    ```
    A API estará disponível em `https://localhost:7123` (ou outra porta similar) e a documentação Swagger em `https://localhost:7123/swagger`.

## Uso da API (Exemplos)

A seguir, exemplos de como interagir com os principais endpoints da API usando `cURL`.

### Criar um Novo Cliente
- **Requisição:** `POST /api/customers`
```sh
curl -X POST "https://localhost:7123/api/customers" \
-H "Content-Type: application/json" \
-d '{
  "fullName": "João da Silva",
  "email": "joao.silva@example.com"
}'
```
- **Resposta Esperada:** `201 Created` com a localização do novo recurso no header `Location`.

### Obter um Cliente por ID
- **Requisição:** `GET /api/customers/{id}`
```sh
curl -X GET "https://localhost:7123/api/customers/1a2b3c4d-5e6f-7g8h-9i0j-1k2l3m4n5o6p"
```
- **Resposta Esperada:** `200 OK` com o corpo JSON do cliente.
```json
{
  "id": "1a2b3c4d-5e6f-7g8h-9i0j-1k2l3m4n5o6p",
  "fullName": "João da Silva",
  "email": "joao.silva@example.com"
}
```

### Criar um Novo Produto
- **Requisição:** `POST /api/products`
```sh
curl -X POST "https://localhost:7123/api/products" \
-H "Content-Type: application/json" \
-d '{
  "name": "Smartphone Pro X",
  "sku": "SPX-2024-BLK",
  "price": 4999.90,
  "currency": "BRL"
}'
```
- **Resposta Esperada:** `201 Created`.

### Obter um Produto por ID
- **Requisição:** `GET /api/products/{id}`
```sh
curl -X GET "https://localhost:7123/api/products/a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
```
- **Resposta Esperada:** `200 OK` com o corpo JSON do produto.
```json
{
  "id": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
  "name": "Smartphone Pro X",
  "sku": "SPX-2024-BLK",
  "price": 4999.90,
  "currency": "BRL"
}
```

### Criar um Novo Pedido
- **Requisição:** `POST /api/orders`
```sh
curl -X POST "https://localhost:7123/api/orders" \
-H "Content-Type: application/json" \
-d '{
  "customerId": "1a2b3c4d-5e6f-7g8h-9i0j-1k2l3m4n5o6p",
  "orderItems": [
    {
      "productId": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
      "quantity": 1
    },
    {
      "productId": "outroid-de-produto-aqui",
      "quantity": 2
    }
  ]
}'
```
- **Resposta Esperada:** `201 Created`.

### Obter um Pedido por ID
- **Requisição:** `GET /api/orders/{id}`
```sh
curl -X GET "https://localhost:7123/api/orders/c4d5e6f7-g8h9-i0j1-k2l3-m4n5o6p7q8r9"
```
- **Resposta Esperada:** `200 OK` com o corpo JSON do pedido.
```json
{
  "id": "c4d5e6f7-g8h9-i0j1-k2l3-m4n5o6p7q8r9",
  "customerId": "1a2b3c4d-5e6f-7g8h-9i0j-1k2l3m4n5o6p",
  "orderDate": "2025-09-05T17:10:19Z",
  "totalAmount": 9999.80,
  "orderItems": [
    {
      "productId": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6",
      "productName": "Smartphone Pro X",
      "quantity": 1,
      "unitPrice": 4999.90
    },
    {
      "productId": "outroid-de-produto-aqui",
      "productName": "Carregador Rápido",
      "quantity": 2,
      "unitPrice": 2499.95
    }
  ]
}
```
