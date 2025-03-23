FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY OrderProcessingService.API/*.csproj OrderProcessingService.API/
COPY OrderProcessingService.Application/*.csproj OrderProcessingService.Application/
COPY OrderProcessingService.Domain/*.csproj OrderProcessingService.Domain/
COPY OrderProcessingService.Infrastructure/*.csproj OrderProcessingService.Infrastructure/

WORKDIR /src/OrderProcessingService.API
RUN dotnet restore

WORKDIR /src
COPY . .

RUN dotnet publish OrderProcessingService.API/OrderProcessingService.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "OrderProcessingService.API.dll"]
