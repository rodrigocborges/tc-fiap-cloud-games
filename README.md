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

## Visualização do banco de dados no Supabase
Resolvi criar essa demonstração de como está o banco de dados em PostgreSQL, após a criação via migrations e inserção de dados usando os endpoints:

![](https://s14.gifyu.com/images/bxvWB.gif)

## Fase 2
<details>
  <summary>Dockerização, CI/CD, APM e Publicação em Cloud</summary>


Nesta fase do projeto, o foco foi garantir que a aplicação FIAP Cloud Games se tornasse escalável, resiliente e observável, preparando-a para um ambiente de produção. Para atingir esses objetivos, a estratégia foi dividida em quatro pilares principais: a preparação da infraestrutura na nuvem, a conteinerização da aplicação com Docker, a automação dos processos de build e deploy com CI/CD, e o monitoramento de performance em tempo real com New Relic.

### 1. Provisionamento da Infraestrutura no Microsoft Azure
A primeira etapa consistiu em preparar o ambiente na nuvem para hospedar a aplicação. Todos os recursos foram organizados dentro de um único Grupo de Recursos (estudos-fiap) para facilitar o gerenciamento.

- **Azure Container Registry (ACR):** Foi provisionado um ACR (`acrfiaprodrigoborges`) para servir como um repositório privado e seguro para as imagens Docker da nossa aplicação. O uso de um registro de contêiner é fundamental para um pipeline de implantação contínua (CD).

- **App Service e App Service Plan:** Para executar a aplicação, foi criado um App Service configurado para rodar contêineres Linux. Ele foi associado a um App Service Plan do tipo Free, adequado para o ambiente de desenvolvimento e testes. A aplicação ficou acessível publicamente através do endpoint: https://tc-fiap-cloud-games-rodrigo.azurewebsites.net.

### 2. Conteinerização com Docker e Integração de APM
Para garantir portabilidade e consistência entre os ambientes, a aplicação foi conteinerizada. O `Dockerfile` foi construído utilizando uma abordagem de múltiplos estágios (multi-stage build), resultando em uma imagem final otimizada, contendo apenas o necessário para executar a aplicação.

- Monitoramento com New Relic: Durante a construção da imagem, o agente APM (Application Performance Monitoring) do New Relic para .NET foi instalado. Essa integração, definida diretamente no [Dockerfile](https://github.com/rodrigocborges/tc-fiap-cloud-games/blob/master/Dockerfile), permite a coleta de métricas detalhadas de performance da aplicação, como tempo de resposta de transações, taxa de erros e saúde da aplicação, sem a necessidade de instrumentação manual do código.

### 3. Automação de Build e Deploy com GitHub Actions (CI/CD)
O pilar central da automação foi a implementação de pipelines de Integração e Implantação Contínua utilizando GitHub Actions.
- **Segurança e Credenciais:** Para permitir que o GitHub Actions interagisse de forma segura com o Azure, foi gerada uma credencial de serviço (Service Principal) através do Azure CLI. Todas as informações sensíveis — credenciais do Azure, usuário e senha do ACR, Connection String do banco de dados e chaves de configuração do JWT e New Relic — foram armazenadas de forma segura como Secrets no repositório do GitHub.

- [Workflow de CI (Continuous Integration)](https://github.com/rodrigocborges/tc-fiap-cloud-games/blob/master/.github/workflows/ci.yml): Este pipeline é acionado a cada push ou pull request na `master`. Sua função é compilar o projeto e executar a suíte de testes unitários, garantindo que novas alterações não introduzam regressões no código.

- [Workflow de CD (Continuous Deployment)](https://github.com/rodrigocborges/tc-fiap-cloud-games/blob/master/.github/workflows/cd.yml): Acionado automaticamente após um merge na branch `master`, este pipeline orquestra todo o processo de deploy:
  - Realiza o build da imagem Docker.
  - Autentica-se e envia a nova imagem para o Azure Container Registry (ACR).
  - Instrui o Azure App Service a realizar o deploy utilizando a imagem recém-publicada.
  - Configura as variáveis de ambiente da aplicação (como connection strings e chaves de API), injetando os valores armazenados nos Secrets do GitHub.


> Com essa automação, todo o processo de entrega da aplicação, desde o commit do código até a publicação em produção, é realizado de forma rápida, segura e padronizada.

### Um pequeno histórico de comandos que executei nessa fase

- Obtenção do JSON do Azure Credentials:
```bash
az ad sp create-for-rbac --name "github-actions-fiapcloudgames" --role contributor --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group-name} --sdk-auth
```

- Testar a publicação no ACR:
```bash
docker build -t tcfiapcloudgamesrodrigoborges:latest .

docker tag tcfiapcloudgamesrodrigoborges:latest acrfiaprodrigoborges.azurecr.io/tcfiapcloudgamesrodrigoborges:latest
docker push acrfiaprodrigoborges.azurecr.io/tcfiapcloudgamesrodrigoborges:latest

#Opcional, para testar se dá para dar pull na imagem que está no repositório
docker pull acrfiaprodrigoborges.azurecr.io/tcfiapcloudgamesrodrigoborges
```
</details>

## Links
- Link do diagrama do Event Storming: https://drive.google.com/file/d/1jkNFj9CuH-e0h5yEg13T9t7zMnOurY5L/view?usp=sharing (caso fique ruim a visualização, disponibilizei uma imagem PNG de alta resolução https://drive.google.com/file/d/1S1eaRvYbELy1IGKgYbXT7jXZvY-pGg_t/view?usp=sharing) 
