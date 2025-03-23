# Usa a imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define a raiz do projeto como diretório de trabalho
WORKDIR /app

# Configura o NuGet
RUN dotnet nuget list source || dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# Copia os arquivos da solução e os .csproj
COPY *.sln ./
COPY OrderProcessingService.API/*.csproj OrderProcessingService.API/
COPY OrderProcessingService.Application/*.csproj OrderProcessingService.Application/
COPY OrderProcessingService.Domain/*.csproj OrderProcessingService.Domain/
COPY OrderProcessingService.Infrastructure/*.csproj OrderProcessingService.Infrastructure/
COPY OrderProcessingService.Worker/*.csproj OrderProcessingService.Worker/

# Restaura as dependências
RUN dotnet nuget locals all --clear
RUN dotnet restore OrderProcessingService.sln

# Copia o restante do código-fonte
COPY . .

# Publica a API e o Worker (sem --no-restore)
RUN dotnet publish OrderProcessingService.API/OrderProcessingService.API.csproj -c Release -o /out/api
RUN dotnet publish OrderProcessingService.Worker/OrderProcessingService.Worker.csproj -c Release -o /out/worker

# Runtime para execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os binários publicados
COPY --from=build /out/api ./api
COPY --from=build /out/worker ./worker

# Expõe a porta da API
EXPOSE 5000

# Comando de inicialização
CMD ["dotnet", "api/OrderProcessingService.API.dll"]
