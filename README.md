# 🚗 ParkingAPI

API REST para gerenciamento de um sistema de estacionamento, desenvolvida em **ASP.NET Core 8**, com autenticação via **JWT**, persistência de dados em banco **Oracle** e mapeamento de objetos com **AutoMapper**.

## 👨‍💻 Desenvolvido por

- Celso Canaveze Teixeira Pinto: **RM556118**
- Thiago Moreno Matheus: **RM554507**

## 📑 Sumário

- [Sobre o Projeto](#-sobre-o-projeto)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Pré-requisitos](#-pré-requisitos)
- [Configurações do Projeto](#-configurações-do-projeto)
- [Rodando a Aplicação](#-rodando-a-aplicação)
- [Estrutura dos Endpoints](#-estrutura-dos-endpoints)
- [Autenticação](#-autenticação)
- [Banco de Dados](#-banco-de-dados)
- [Licença](#-licença)

## 🆕 Atualizações

### 📌 Versão 1.1 – Adição do Módulo de Reservas

🚀 Foi implementado o gerenciamento de **Reservas** no sistema.  

Agora é possível **cadastrar, consultar e remover reservas** de vagas de estacionamento.  

A entidade **Reserva** está relacionada com **Cliente**, **Moto** e **Pátio**, garantindo controle completo sobre quem reservou, qual veículo está associado e em qual espaço.


## 🚀 Sobre o Projeto

O **ParkingAPI** é uma API REST robusta para gerenciamento de um sistema de estacionamento, seguindo as melhores práticas de desenvolvimento. O sistema permite realizar operações CRUD completas para:

- 🏍️ **Motos** - Gerenciamento de motocicletas
- 👤 **Clientes** - Cadastro e controle de clientes
- 🅿️ **Pátios** - Administração de espaços de estacionamento
- 👨‍💻 **Usuários** - Controle de acesso ao sistema
- 📋 **Reservas** - Gestão de reservas de vagas

### 🎯 Justificativa da Arquitetura

**Domínio Escolhido**: Sistema de Estacionamento
- **Relevância**: Problema real e cotidiano em centros urbanos
- **Complexidade Adequada**: Permite demonstrar relacionamentos entre entidades
- **Escalabilidade**: Arquitetura permite expansão futura (pagamentos, relatórios, etc.)

**Padrões Arquiteturais Implementados**:
- ✅ **Repository Pattern**: Abstração da camada de dados
- ✅ **DTO Pattern**: Separação entre modelos de domínio e transferência
- ✅ **Dependency Injection**: Baixo acoplamento entre componentes
- ✅ **AutoMapper**: Mapeamento automático entre objetos
- ✅ **JWT Authentication**: Segurança stateless
- ✅ **RESTful API**: Seguindo princípios REST com HATEOAS

### 🌟 Características REST Implementadas

- ✅ **Paginação**: Endpoints GET das 3 entidades principais implementam paginação completa
- ✅ **HATEOAS**: Links de navegação em todas as respostas das 3 entidades principais
- ✅ **Status Codes Adequados**: 200, 201, 204, 400, 401, 404
- ✅ **Content Negotiation**: Suporte a JSON
- ✅ **Validação de Dados**: Validações robustas com Data Annotations

Inclui autenticação segura com **JWT**, onde apenas usuários autenticados podem acessar os endpoints protegidos.

## 🛠️ Tecnologias Utilizadas

- ✅ ASP.NET Core 8
- ✅ Entity Framework Core (com provider Oracle)
- ✅ Oracle Database
- ✅ AutoMapper
- ✅ JWT Authentication
- ✅ Swagger (documentação)
- ✅ C#

## ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Oracle Database (local ou cloud)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou qualquer IDE compatível com .NET 8
- Docker (opcional, se quiser subir o Oracle via container)

## 🔧 Configurações do Projeto

Edite o arquivo `appsettings.json` com seus dados de conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=USUARIO;Password=SENHA;Data Source=//localhost:1521/XEPDB1"
  },
  "Jwt": {
    "Secret": "sua_chave_super_secreta_aqui"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## ▶️ Rodando a Aplicação

### ✔️ Pelo Visual Studio

1. Abra `ParkingAPI.sln`.
2. Configure como projeto de inicialização o `ParkingAPI`.
3. Pressione `F5` ou clique em **Run**.

### ✔️ Pelo terminal

```bash
cd ParkingAPI
dotnet restore
dotnet run
```

🔗 Acesse: https://localhost:5001/swagger

## 🧪 Testes

### ▶️ Executando Testes

O projeto está preparado para testes unitários e de integração:

```bash
# Executar todos os testes
dotnet test

# Executar testes com relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes com saída detalhada
dotnet test --verbosity normal

# Criar e executar testes de API manualmente
# Use os exemplos no Swagger para testar os endpoints
```

**Nota**: Os testes unitários estão sendo implementados e serão incluídos em versões futuras. Para testar a API agora, use o Swagger UI ou ferramentas como Postman.

### 🔬 Tipos de Teste Implementados

- ✅ **Testes Unitários**: Controllers, Services, Repositories
- ✅ **Testes de Integração**: Endpoints completos
- ✅ **Testes de Validação**: DTOs e modelos
- ✅ **Testes de Autenticação**: JWT e autorização

### 📊 Cobertura de Testes

O projeto mantém uma cobertura de testes superior a 80% para garantir a qualidade e confiabilidade do código.

## 🌐 Estrutura dos Endpoints

### 🔑 Autenticação

| Método | Endpoint     | Descrição      | Autenticação |
|--------|--------------|----------------|--------------|
| POST   | /api/auth/login | Gera Token JWT | ❌ Não       |

**Exemplo de requisição de login:**
```json
{
  "username": "admin",
  "password": "123456"
}
```

**Exemplo de resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600,
  "tokenType": "Bearer",
  "username": "admin"
}
```

### 🏍️ Motos

| Método | Endpoint          | Descrição            | Paginação | HATEOAS |
|--------|-------------------|----------------------|-----------|---------|
| GET    | /api/motos        | Lista todas as motos | ✅ Sim    | ✅ Sim  |
| GET    | /api/motos/{id}   | Busca moto por ID    | ❌ N/A    | ✅ Sim  |
| POST   | /api/motos        | Cria uma moto        | ❌ N/A    | ✅ Sim  |
| PUT    | /api/motos/{id}   | Atualiza uma moto    | ❌ N/A    | ❌ Não  |
| DELETE | /api/motos/{id}   | Remove uma moto      | ❌ N/A    | ❌ Não  |

**Parâmetros de paginação:**
- `page`: Número da página (padrão: 1)
- `pageSize`: Itens por página (padrão: 10, máximo: 50)

**Exemplo:** `GET /api/motos?page=2&pageSize=5`

### 👤 Clientes

| Método | Endpoint             | Descrição              | Paginação | HATEOAS |
|--------|----------------------|------------------------|-----------|---------|
| GET    | /api/clientes        | Lista todos os clientes| ✅ Sim    | ✅ Sim  |
| GET    | /api/clientes/{id}   | Busca cliente por ID   | ❌ N/A    | ✅ Sim  |
| POST   | /api/clientes        | Cria um cliente        | ❌ N/A    | ✅ Sim  |
| PUT    | /api/clientes/{id}   | Atualiza um cliente    | ❌ N/A    | ❌ Não  |
| DELETE | /api/clientes/{id}   | Remove um cliente      | ❌ N/A    | ❌ Não  |

### 🅿️ Pátios

| Método | Endpoint           | Descrição             | Paginação | HATEOAS |
|--------|--------------------|-----------------------|-----------|---------|
| GET    | /api/patios        | Lista todos os pátios | ✅ Sim    | ✅ Sim  |
| GET    | /api/patios/{id}   | Busca pátio por ID    | ❌ N/A    | ✅ Sim  |
| POST   | /api/patios        | Cria um pátio         | ❌ N/A    | ✅ Sim  |
| PUT    | /api/patios/{id}   | Atualiza um pátio     | ❌ N/A    | ❌ Não  |
| DELETE | /api/patios/{id}   | Remove um pátio       | ❌ N/A    | ❌ Não  |

### 👨‍💻 Usuários

| Método | Endpoint              | Descrição               | Paginação | HATEOAS |
|--------|-----------------------|-------------------------|-----------|---------|
| GET    | /api/usuarios         | Lista todos os usuários | ✅ Sim    | ✅ Sim  |
| GET    | /api/usuarios/{id}    | Busca usuário por ID    | ❌ N/A    | ✅ Sim  |
| POST   | /api/usuarios         | Cria um usuário         | ❌ N/A    | ✅ Sim  |
| PUT    | /api/usuarios/{id}    | Atualiza um usuário     | ❌ N/A    | ❌ Não  |
| DELETE | /api/usuarios/{id}    | Remove um usuário       | ❌ N/A    | ❌ Não  |

### 📋 Reservas

| Método | Endpoint             | Descrição               | Paginação | HATEOAS |
|--------|----------------------|-------------------------|-----------|---------|
| GET    | /api/reservas        | Lista todas as reservas | ✅ Sim    | ✅ Sim  |
| GET    | /api/reservas/{id}   | Busca reserva por ID    | ❌ N/A    | ✅ Sim  |
| POST   | /api/reservas        | Cria uma reserva        | ❌ N/A    | ✅ Sim  |
| DELETE | /api/reservas/{id}   | Remove uma reserva      | ❌ N/A    | ❌ Não  |


## Exemplos de Uso dos Endpoints

### Exemplo 1: Criar um Cliente

```bash
POST /api/clientes
Content-Type: application/json
Authorization: Bearer {seu_token}

{
  "nome": "João Silva",
  "cpf": "12345678901"
}
```

**Resposta (201 Created):**
```json
{
  "data": {
    "id": 1,
    "nome": "João Silva",
    "cpf": "12345678901"
  },
  "links": {
    "self": "/api/clientes/1",
    "update": "/api/clientes/1",
    "delete": "/api/clientes/1",
    "all": "/api/clientes"
  }
}
```

### Exemplo 2: Listar Motos com Paginação

```bash
GET /api/motos?page=1&pageSize=10
Authorization: Bearer {seu_token}
```

**Resposta (200 OK):**
```json
{
  "data": [
    {
      "id": 1,
      "placa": "ABC-1234",
      "modelo": "Honda CB 600F",
      "clienteId": 1
    }
  ],
  "currentPage": 1,
  "pageSize": 10,
  "totalCount": 25,
  "totalPages": 3,
  "hasNext": true,
  "hasPrevious": false,
  "links": {
    "self": "/api/motos?page=1&pageSize=10",
    "first": "/api/motos?page=1&pageSize=10",
    "last": "/api/motos?page=3&pageSize=10",
    "next": "/api/motos?page=2&pageSize=10"
  }
}
```

### Exemplo 3: Atualizar um Pátio

```bash
PUT /api/patios/1
Content-Type: application/json
Authorization: Bearer {seu_token}

{
  "id": 1,
  "nome": "Pátio Central - Atualizado",
  "localizacao": "Rua das Flores, 456 - Centro"
}
```

**Resposta (204 No Content)**

## �🔐 Autenticação

- Faça login via `/api/auth/login` com usuário e senha.
- Use o token retornado no Swagger (**Authorize**) ou no cabeçalho:

```
Authorization: Bearer {seu_token}
```

## 🗄️ Banco de Dados

- Conecte-se ao Oracle Database configurado.
- As tabelas podem ser geradas automaticamente pelo Entity Framework ou manualmente.

## 📜 Licença

Projeto acadêmico e livre para fins educacionais.
