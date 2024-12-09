# 使用官方的 ASP.NET Core 鏡像作為基礎
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# 使用官方的 .NET SDK 鏡像作為構建環境
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["<YourProjectName>.csproj", "./"]
RUN dotnet restore "./<YourProjectName>.csproj"
COPY . .
RUN dotnet publish "./<YourProjectName>.csproj" -c Release -o /app/publish

# 將構建好的應用程式部署到執行環境
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "StudentManagement.dll"]
