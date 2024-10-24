# ChurchStore

**ChurchStore** é uma aplicação desenvolvida para facilitar as reservas na cantina da **Livres Church**, com o objetivo de reduzir filas durante as compras. A plataforma permite que os usuários realizem pedidos antecipadamente e que os administradores gerenciem os pedidos e visualizem estatísticas sobre o consumo.

## Funcionalidades

### Para Usuários
- Realização de reservas de itens disponíveis na cantina.
- Visualização de pedidos realizados.
- Atualização e cancelamento de pedidos, conforme permitido.

### Para Administradores
- Gerenciamento completo de pedidos.
- Acesso a estatísticas e relatórios sobre as vendas.
- Controle de estoque e disponibilidade de produtos.
  
## Tecnologias Utilizadas

- **.NET Core 8**: Backend robusto para a API e controle de funcionalidades.
- **Bootstrap**: Estilização responsiva e layout da aplicação.
- **jQuery**: Facilitação de interações dinâmicas no front-end.
- **FontAwesome**: Ícones modernos e elegantes para melhorar a experiência do usuário.

## Estrutura da Aplicação

A aplicação está dividida em dois tipos de login:

- **Login de Usuário**: Interface onde os membros da igreja podem fazer pedidos de itens da cantina.
- **Login de Admin**: Painel administrativo que permite controlar e gerenciar todos os pedidos, além de analisar dados e estatísticas sobre as vendas.

## Como Executar o Projeto

### Pré-requisitos

- **.NET Core 8 SDK** instalado
- **Node.js** (opcional para gerenciamento de pacotes front-end)

## Configuração do Projeto

Antes de rodar o projeto, é necessário criar um arquivo `appsettings.json` na raiz de ambos os projetos **ChurchStore.Api** e **ChurchStore.ApiAdmin** com o seguinte conteúdo:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "MySqlConnection": "Server=localhost;Port=port;Database=church_store;Uid=user;Pwd=password;charset=utf8;"
  },
  "EmailSettings": {
    "EmailNome": "Livres Church",
    "EmailRemetente": "email",
    "Senha": "senha de app",
    "SmtpServer": "smtp.gmail.com",
    "Porta": 465
  },
  "TokenSettings": {
    "Key": "chave"
  },
  "AllowedHosts": "*"
}
``` 

## Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para enviar um pull request ou abrir uma issue para melhorias e sugestões.

## Licença
Este projeto é licenciado sob a MIT License.

ChurchStore - Livres Church © 2024
