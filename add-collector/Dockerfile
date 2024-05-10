# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["otel-example-app-add-collector.csproj", "./"]
RUN dotnet restore "otel-example-app-add-collector.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "otel-example-app-add-collector.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "otel-example-app-add-collector.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "otel-example-app-add-collector.dll"]
