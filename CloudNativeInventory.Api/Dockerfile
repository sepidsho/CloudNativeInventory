# مرحله 1: استفاده از SDK مایکروسافت برای ساخت (Build) پروژه
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# کپی کردن فایل‌های پروژه و نصب بسته‌ها
COPY ["CloudNativeInventory.Api/CloudNativeInventory.Api.csproj", "CloudNativeInventory.Api/"]
RUN dotnet restore "CloudNativeInventory.Api/CloudNativeInventory.Api.csproj"

# کپی کردن کل کدها و ساخت نسخه نهایی
COPY . .
WORKDIR "/src/CloudNativeInventory.Api"
RUN dotnet publish -c Release -o /app/publish

# مرحله 2: ساخت ایمیج نهایی که سبک و آماده اجراست
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# تنظیم پورت (برای .NET 9 پورت 8080 استاندارد است)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CloudNativeInventory.Api.dll"]