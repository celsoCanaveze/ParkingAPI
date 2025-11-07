🚗 ParkingAPI

API REST para gerenciamento de um sistema de estacionamento, desenvolvida em ASP.NET Core 8, com autenticação via JWT, persistência de dados em banco Oracle e mapeamento de objetos com AutoMapper.

👨‍💻 Desenvolvido por

Celso Canaveze Teixeira Pinto — RM556118

Thiago Moreno Matheus — RM554507

📑 Sumário

Sobre o Projeto

Tecnologias Utilizadas

Pré-requisitos

Configurações do Projeto

Rodando a Aplicação

Endpoints Principais

Autenticação

Banco de Dados

Health Check

Versionamento da API

Machine Learning (ML.NET)

Testes Automatizados

Licença

🆕 Atualizações — Versão 2.0

📦 Novidades incluídas nesta versão:

✅ Endpoint de Health Check (/health)

✅ Versionamento da API (v1 e v2)

✅ Segurança JWT revisada com middleware atualizado

✅ Integração com ML.NET para previsão de valores de reserva

✅ Testes unitários e de integração com xUnit

✅ README e Swagger atualizados

🚀 Sobre o Projeto

O ParkingAPI é uma API REST robusta para gerenciamento de um sistema de estacionamento, seguindo as melhores práticas de arquitetura e segurança.

Permite operações CRUD completas para:

🏍️ Motos

👤 Clientes

🅿️ Pátios

👨‍💻 Usuários

📋 Reservas

Além disso, agora conta com:

🩺 Health Checks

🧩 Versionamento (v1 e v2)

🔐 Autenticação JWT

🤖 Endpoint de Machine Learning

🛠️ Tecnologias Utilizadas

✅ ASP.NET Core 8 (Web API)

✅ Entity Framework Core (Oracle Provider)

✅ AutoMapper

✅ Swagger

✅ JWT Authentication

✅ ML.NET

✅ xUnit

⚙️ Pré-requisitos

.NET 8 SDK

Oracle Database (local ou cloud)

Visual Studio 2022 / VS Code

Docker (opcional)

🔧 Configurações do Projeto

Configure o arquivo appsettings.json:

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

▶️ Rodando a Aplicação
💻 Via Terminal
cd ParkingAPI
dotnet restore
dotnet run


Acesse o Swagger:
👉 https://localhost:5001/swagger

📡 Endpoints Principais
Categoria	Endpoint Base	Autenticação	Versão
Auth	/api/auth	❌	v1
Clientes	/api/v1/clientes	✅	v1
Motos	/api/v1/motos	✅	v1
Pátios	/api/v1/patios	✅	v1
Usuários	/api/v1/usuarios	✅	v1
Reservas	/api/v1/reservas	✅	v1
Health	/health	❌	Global
ML.NET	/api/v1/ml/predict	✅	v1
🔐 Autenticação (JWT)

Faça login via:

POST /api/auth/login

{
  "username": "admin",
  "password": "123456"
}


Resposta:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600,
  "tokenType": "Bearer"
}


Envie o token nas requisições:

Authorization: Bearer {seu_token}

🩺 Health Check

GET /health
Retorna o status da aplicação:

{
  "status": "Healthy"
}

🧩 Versionamento da API

A API suporta múltiplas versões:

Versão	Exemplo de Rota
v1	/api/v1/reservas
v2	/api/v2/reservas
🤖 Machine Learning (ML.NET)

POST /api/v1/ml/predict

Entrada:

{
  "tempoEstacionado": 3,
  "valorPorHora": 10
}


Resposta:

{
  "valorPrevisto": 30
}


Esse endpoint demonstra o uso do ML.NET para simular uma predição de valor total baseado em parâmetros enviados.

🧪 Testes Automatizados

O projeto contém testes unitários e de integração com xUnit.

▶️ Executando os Testes
# Rodar todos os testes
dotnet test

# Com cobertura de código
dotnet test --collect:"XPlat Code Coverage"


Estrutura dos testes:

Unitários: Tests/Unit/

Integração: Tests/Integration/

🔍 Testes Implementados

✅ Lógica principal de Reserva (unitário)

✅ Health Check (integração)

✅ ML.NET (unitário)

✅ Autenticação JWT (unitário)

📘 Documentação Swagger

Após rodar o projeto, acesse:
👉 http://localhost:5000/swagger

A documentação inclui todas as versões da API e exemplos de requisições autenticadas.

🗄️ Banco de Dados

Banco de dados Oracle com criação de tabelas via Entity Framework Migrations.
Relacionamentos implementados até 3FN (Terceira Forma Normal).

📜 Licença

Projeto acadêmico desenvolvido para fins educacionais.
© 2025 — Celso Canaveze & Thiago Moreno
