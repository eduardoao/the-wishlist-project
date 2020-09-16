
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Authenticator.Api.csproj", ""]
RUN dotnet restore "./Authenticator.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Authenticator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authenticator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Wishlist.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Authenticator.Api.dll

#https://jakubwajs.wordpress.com/2020/01/31/deploying-asp-net-core-3-1-web-api-to-heroku-with-docker/
