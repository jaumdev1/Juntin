# Use a imagem base do SDK .NET para construir o aplicativo e executar as migrações
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instale o dotnet-ef globalmente
RUN dotnet tool install --global dotnet-ef

# Adicione o caminho de instalação das ferramentas globais do .NET ao PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Restaure as dependências e ferramentas do projeto
COPY ["Juntin.Presentation/Juntin.Presentation.csproj", "Juntin.Presentation/"]
COPY ["Juntin.Application/Juntin.Application.csproj", "Juntin.Application/"]
COPY ["Juntin.Infrastructure/Juntin.Infrastructure.csproj", "Juntin.Infrastructure/"]
COPY ["Juntin.Domain/Juntin.Domain.csproj", "Juntin.Domain/"]
RUN dotnet restore "Juntin.Presentation/Juntin.Presentation.csproj"

# Copie os arquivos e construa o projeto
COPY . .
WORKDIR "/src/Juntin.Presentation"
RUN dotnet build "Juntin.Presentation.csproj" -c Release -o /app/build
# Publique o projeto
RUN dotnet publish "Juntin.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use a imagem base do ASP.NET para executar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
# Copie o script de entrada

ENTRYPOINT ["dotnet", "Juntin.Presentation.dll"]
