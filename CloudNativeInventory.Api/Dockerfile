FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CloudNativeInventory.Api/CloudNativeInventory.Api.csproj", "CloudNativeInventory.Api/"]
RUN dotnet restore "CloudNativeInventory.Api/CloudNativeInventory.Api.csproj"

COPY . .
WORKDIR "/src/CloudNativeInventory.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish . 
USER app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CloudNativeInventory.Api.dll"]
