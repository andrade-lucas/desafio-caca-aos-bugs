![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg)

## 🎖️ Desafio
**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe __NullReferenceException__ onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto
Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes
### 🚀 Líder Técnico
[Lucas Andrade e Silva](https://github.com/andrade-lucas) 
* [![Linkedin](https://i.sstatic.net/gVE0j.png) LinkedIn](https://www.linkedin.com/in/lucas-andrade-e-silva/)

### 👻 Caçadores de Bugs
* [Lucas Andrade e Silva](https://github.com/andrade-lucas)

## ⚙️ Tecnologias
* C# 12
* .NET 8
* ASP.NET
* Minimal APIs
* Blazor Web Assembly
* xUnit

## 🥋 Skills Desenvolvidas
* Comunicação
* Trabalho em Equipe
* Networking
* Muito conhecimento técnico

## 🧪 Como testar o projeto
### Dima / bugs
Abra o projeto `/bugs` no seu editor e adicione a string de conexão do seu banco no arquivo `appsettings.json`, na propriedade `ConnectionStrings:DefaultConnection` do projeto `Dima.Api`.

Em seguida você deve:
 * instalar a o [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) no seu computador;
 * navegar para o diretório do projeto `Dima.Api` e executar o comando `dotnet ef database update` para criar o banco de dados da aplicação;
 * dentro de `Dima.Api/Data/Views` existem alguns arquivos `.sql`, você deve copiar o conteúdo deles e rodar no banco de dados para gerar as views da aplicação
 * você deve abrir um terminal no diretório do repositório e rodar o projeto `Dima.Api`:
   * `cd bugs/Dima.Api`
   * `dotnet run`
 * em seguida o projeto `Dima.Web`, em outro terminal:
   * `cd bugs/Dima.Web`
   * `dotnet run`
 * abra o seu navegador e navegue para o endereço que é exibido no seu terminal do projeto `Dima.Web`.

### Balta / unit-tests
Abra o projeto `/unit-tests` no seu editor e execute os testes

Abra um terminal e execute `dotnet test` ou clique com o direitor do mouse no projeto de testes dentro do seu editor e clique em `Run Tests`

# 💜 Participe
Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)
