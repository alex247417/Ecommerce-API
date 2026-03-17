# 🛒 Ecommerce-API

API REST completa para sistema de e-commerce desenvolvida com **ASP.NET Core** e **C#**, com autenticação JWT, integração com **Stripe** para simulação de pagamentos com cartão e validação de transações.

## 🚀 Tecnologias

- C# / .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Stripe API (simulação de pagamento)
- Swagger / OpenAPI

## ✨ Funcionalidades

- **Autenticação JWT** — cadastro, login e proteção de rotas
- **Produtos** — cadastro, listagem, edição e remoção
- **Pedidos** — criação e consulta de pedidos
- **Carrinho** — adição e remoção de itens
- **Pagamentos** — integração com Stripe para simular cobrança em cartão de crédito
- **Validação de transações** — confirmação e tratamento de status de pagamento
- **Documentação** — endpoints documentados via Swagger UI com suporte a Bearer token

## 📦 Como rodar localmente

```bash
# Clone o repositório
git clone https://github.com/alex247417/Ecommerce-API.git
cd Ecommerce-API

# Restaure os pacotes
dotnet restore

# Configure as variáveis em appsettings.json:
# - ConnectionStrings:DefaultConnection
# - Jwt:Key e Jwt:Issuer
# - Stripe:SecretKey (use a chave de teste do Stripe)

# Atualize o banco de dados
dotnet ef database update

# Execute a aplicação
dotnet run
```

Acesse `https://localhost:5001/swagger` para explorar os endpoints.

> 💡 Para testar pagamentos use o cartão de teste do Stripe: `4242 4242 4242 4242`

## 🗂 Estrutura do projeto

```
Ecommerce-API/
├── Controllers/       # Endpoints da API
├── Models/            # Entidades do domínio
├── Data/              # Contexto do Entity Framework
├── DTOs/              # Objetos de transferência de dados
├── Services/          # Lógica de negócio e integração Stripe
└── Program.cs
```

## 📡 Endpoints principais

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/register` | Cadastro de usuário |
| POST | `/api/auth/login` | Login e geração de token JWT |
| GET | `/api/produtos` | Lista todos os produtos |
| POST | `/api/produtos` | Cadastra novo produto |
| PUT | `/api/produtos/{id}` | Atualiza produto |
| DELETE | `/api/produtos/{id}` | Remove produto |
| POST | `/api/pedidos` | Cria novo pedido |
| GET | `/api/pedidos/{id}` | Consulta pedido |
| POST | `/api/pagamentos/checkout` | Processa pagamento via Stripe |
| GET | `/api/pagamentos/{id}` | Consulta status do pagamento |

## 🔐 Autenticação

Rotas protegidas exigem JWT Bearer Token:

1. Faça `POST /api/auth/login` com email e senha
2. Copie o token da resposta
3. No Swagger clique em **Authorize** e cole `Bearer {token}`

## 👨‍💻 Autor

**Alecsandro da Silva Negrão Pascoal**  
[LinkedIn](https://linkedin.com/in/alecsandro-silva-dev) · [GitHub](https://github.com/alex247417)
