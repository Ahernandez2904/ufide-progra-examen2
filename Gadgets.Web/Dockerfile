#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gadgets.Web/Gadgets.Web.csproj", "Gadgets.Web/"]
COPY ["Gadgets.Infrastructure/Gadgets.Infrastructure.csproj", "Gadgets.Infrastructure/"]
COPY ["Gadgets.Domain/Gadgets.Domain.csproj", "Gadgets.Domain/"]
COPY ["Gadgets.Application/Gadgets.Application.csproj", "Gadgets.Application/"]
RUN dotnet restore "Gadgets.Web/Gadgets.Web.csproj"
COPY . .
WORKDIR "/src/Gadgets.Web"
RUN dotnet build "Gadgets.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gadgets.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gadgets.Web.dll"]