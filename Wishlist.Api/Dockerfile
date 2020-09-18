#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wishlist.Api/Wishlist.Api.csproj", "Wishlist.Api/"]
COPY ["Wishlist.Data/Wishlist.Data.csproj", "Wishlist.Data/"]
COPY ["Wishlist.Core/Wishlist.Core.csproj", "Wishlist.Core/"]
RUN dotnet restore "Wishlist.Api/Wishlist.Api.csproj"
COPY . .
WORKDIR "/src/Wishlist.Api"
RUN dotnet build "Wishlist.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wishlist.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wishlist.Api.dll"]