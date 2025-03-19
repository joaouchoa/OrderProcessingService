# Usa a imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define a raiz do projeto como diretório de trabalho
WORKDIR /app

# Configura o cache de pacotes NuGet
#ENV NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages
# Configura o cache do NuGet dentro do container
RUN dotnet nuget list source || dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# Copia a solução e os projetos primeiro
COPY *.sln ./
COPY OrderProcessingService.API/*.csproj OrderProcessingService.API/
COPY OrderProcessingService.Application/*.csproj OrderProcessingService.Application/
COPY OrderProcessingService.Domain/*.csproj OrderProcessingService.Domain/
COPY OrderProcessingService.Infrastructure/*.csproj OrderProcessingService.Infrastructure/
COPY OrderProcessingService.Worker/*.csproj OrderProcessingService.Worker/

# Restaura as dependências e força a atualização dos pacotes
RUN dotnet nuget locals all --clear
RUN dotnet restore OrderProcessingService.sln

# Copia todo o código-fonte
COPY . .

# Publica a API e o Worker
RUN dotnet publish OrderProcessingService.API/OrderProcessingService.API.csproj -c Release -o /out/api --no-restore
RUN dotnet publish OrderProcessingService.Worker/OrderProcessingService.Worker.csproj -c Release -o /out/worker --no-restore

# Usa a imagem .NET Runtime para rodar os serviços
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copia os binários da API e do Worker
COPY --from=build /out/api ./api
COPY --from=build /out/worker ./worker

# Expõe a porta da API
EXPOSE 5000

# Configuração de entrada
CMD ["dotnet", "api/OrderProcessingService.API.dll"]
