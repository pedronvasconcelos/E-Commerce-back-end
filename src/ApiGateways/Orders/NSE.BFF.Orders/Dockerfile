#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/Orders/NSE.BFF.Orders/NSE.BFF.Orders.csproj", "src/ApiGateways/Orders/NSE.BFF.Orders/"]
RUN dotnet restore "src/ApiGateways/Orders/NSE.BFF.Orders/NSE.BFF.Orders.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Orders/NSE.BFF.Orders"
RUN dotnet build "NSE.BFF.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.BFF.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.BFF.Orders.dll"]