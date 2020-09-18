# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.sln .

COPY Wishlist.Api/*.csproj  Wishlist.Api/
COPY Wishlist.Data/*.csproj ./Wishlist.Data/
COPY Wishlist.Core/*.csproj ./Wishlist.Core/
COPY Wishlist.Test/*.csproj Wishlist.Test/

RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/Wishlist.Api
RUN dotnet build
WORKDIR /src/Wishlist.Test
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/Wishlist.Api
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .


# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Wishlist.Api.dll