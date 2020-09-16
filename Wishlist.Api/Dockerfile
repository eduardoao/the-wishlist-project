#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wishlist.Api.csproj", ""]
RUN dotnet restore "./Wishlist.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Wishlist.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wishlist.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Wishlist.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Wishlist.Api.dll

#https://jakubwajs.wordpress.com/2020/01/31/deploying-asp-net-core-3-1-web-api-to-heroku-with-docker/
