## Tech Challenge - FIAP Cloud Games

Pós Tech em Arquitetura em Sistemas .NET.

## Iniciando o projeto usando Minimal APIs
Criei uma solução e coloquei vários projetos lá dentro para separar bem os contextos. Criei os seguintes projetos: 
- Domain: Possui todas interfaces, entidades, enumeradores e objetos de valor.
- Infrastructure: Possui toda a parte de infraestrutura em si, possui o contexto do banco de dados, as migrações e a implementação em si dos meus repositórios.
- Tests: nesse projeto irão ficar todos os testes, principalmente ao construir as entidades e também os objetos de valor como: Email e Senha, por exemplo.
- Application: vão estar presente os casos de uso, os DTOs e serviços da aplicação. 
- API: vão estar os controllers e os middlewares.
- SharedKernel: Nesse projeto é algo opcional, vão estar os tipos genéricos e utilitários compartilhados.


Estou usando .NET Core 9 e PostgreSQL, pois quis experimentar na prática o Supabase via connection string.

<details>
  <summary>Um pouco do meu histórico de comandos para iniciar o desenvolvimento</summary>
  
```bash
mkdir FIAPCloudGames
cd FIAPCloudGames

dotnet new sln -n FIAPCloudGames

dotnet new classlib -n FIAPCloudGames.Domain
dotnet new classlib -n FIAPCloudGames.Application
dotnet new classlib -n FIAPCloudGames.Infrastructure
dotnet new webapi    -n FIAPCloudGames.API
dotnet new classlib -n FIAPCloudGames.SharedKernel
dotnet new xunit     -n FIAPCloudGames.Tests
```

Após isso, adicionei os projetos a solução e referenciei devidamente os projetos:

```bash
dotnet sln add FIAPCloudGames.Domain/FIAPCloudGames.Domain.csproj
dotnet sln add FIAPCloudGames.Application/FIAPCloudGames.Application.csproj
dotnet sln add FIAPCloudGames.Infrastructure/FIAPCloudGames.Infrastructure.csproj
dotnet sln add FIAPCloudGames.API/FIAPCloudGames.API.csproj
dotnet sln add FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj
dotnet sln add FIAPCloudGames.Tests/FIAPCloudGames.Tests.csproj

# Domain depende de SharedKernel
dotnet add FIAPCloudGames.Domain/FIAPCloudGames.Domain.csproj reference FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj

# Application depende de Domain e SharedKernel
dotnet add FIAPCloudGames.Application/FIAPCloudGames.Application.csproj reference FIAPCloudGames.Domain/FIAPCloudGames.Domain.csproj
dotnet add FIAPCloudGames.Application/FIAPCloudGames.Application.csproj reference FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj

# Infrastructure depende de Application, Domain e SharedKernel
dotnet add FIAPCloudGames.Infrastructure/FIAPCloudGames.Infrastructure.csproj reference FIAPCloudGames.Application/FIAPCloudGames.Application.csproj
dotnet add FIAPCloudGames.Infrastructure/FIAPCloudGames.Infrastructure.csproj reference FIAPCloudGames.Domain/FIAPCloudGames.Domain.csproj
dotnet add FIAPCloudGames.Infrastructure/FIAPCloudGames.Infrastructure.csproj reference FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj

# API depende de Application, Infrastructure, SharedKernel
dotnet add FIAPCloudGames.API/FIAPCloudGames.API.csproj reference FIAPCloudGames.Application/FIAPCloudGames.Application.csproj
dotnet add FIAPCloudGames.API/FIAPCloudGames.API.csproj reference FIAPCloudGames.Infrastructure/FIAPCloudGames.Infrastructure.csproj
dotnet add FIAPCloudGames.API/FIAPCloudGames.API.csproj reference FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj

# Tests depende de Domain, Application, SharedKernel
dotnet add FIAPCloudGames.Tests/FIAPCloudGames.Tests.csproj reference FIAPCloudGames.Domain/FIAPCloudGames.Domain.csproj
dotnet add FIAPCloudGames.Tests/FIAPCloudGames.Tests.csproj reference FIAPCloudGames.Application/FIAPCloudGames.Application.csproj
dotnet add FIAPCloudGames.Tests/FIAPCloudGames.Tests.csproj reference FIAPCloudGames.SharedKernel/FIAPCloudGames.SharedKernel.csproj
```

Resolvi usar as seguintes dependências:
```bash
# No Infrastructure (EF Core + PostgreSQL)
dotnet add FIAPCloudGames.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add FIAPCloudGames.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet add FIAPCloudGames.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add FIAPCloudGames.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add FIAPCloudGames.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL

# No API (Swagger e JWT)
dotnet add FIAPCloudGames.API package Swashbuckle.AspNetCore
dotnet add FIAPCloudGames.API package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add FIAPCloudGames.API package Microsoft.EntityFrameworkCore.Design
```  

Já que estou usando uma solução com vários projetos, o comando para rodar migrations é um pouco "diferente":
```bash
dotnet ef migrations add NomeDaMigration --project FIAPCloudGames.Infrastructure --startup-project FIAPCloudGames.API

dotnet ef database update --project FIAPCloudGames.Infrastructure --startup-project FIAPCloudGames.API
```
</details>

## Aplicação de TDD no módulo de gerenciamento de usuários
No desenvolvimento da funcionalidade de criação de usuário, apliquei o processo de Test-Driven Development (TDD) para garantir a qualidade e a robustez do código.

### Passos que segui no TDD:

1. Escrevi o teste unitário CreateUser_WithValidData_ShouldAddUserAndReturnId antes de implementar a lógica no código, garantindo que o método deveria criar um usuário e retornar o ID.

2. Executei o teste e, como ainda não existia o método, ele falhou como esperado.

3. Implementei o método Create no UserService, garantindo que o teste passasse.

4. Refatorei o código, mantendo o teste verde.

5. Adicionei o teste CreateUser_WithDuplicateEmail_ShouldThrowException, também antes da implementação da verificação de duplicidade.

6. Implementei a validação de e-mail duplicado no UserService.

7. Rodei novamente os testes e confirmei que tudo estava funcionando corretamente.

Com isso, demonstrei que a funcionalidade foi construída com base em TDD, orientada pelos testes, evidenciando minha preocupação com a qualidade e a confiabilidade do módulo de gerenciamento de usuários.

_Vale ressaltar que para isso, eu criei um repositório em memória chamado InMemoryUserRepository._

## Links
- Link do diagrama do Event Storming: https://drive.google.com/file/d/1jkNFj9CuH-e0h5yEg13T9t7zMnOurY5L/view?usp=sharing (caso fique ruim a visualização, disponibilizei uma imagem PNG de alta resolução https://drive.google.com/file/d/1S1eaRvYbELy1IGKgYbXT7jXZvY-pGg_t/view?usp=sharing) 
