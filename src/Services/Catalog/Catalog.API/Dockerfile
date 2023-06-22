#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# ASP.NET is a runtime environment that allows you to host and execute ASP.NET web applications. 
WORKDIR /app
# creates dir if it doesn't exist and cd into it
EXPOSE 80
#expose container port

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#he ASP.NET SDK includes the necessary tools and libraries for developing, building, and publishing ASP.NET applications
WORKDIR /src
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"]
# copy Catalog.API.csproj from host machine to folder in container
RUN dotnet restore "Services/Catalog/Catalog.API/Catalog.API.csproj"
# install dependencies
COPY . .
# copy things from host root dir to src/
WORKDIR "/src/Services/Catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build
# this might be unnessecary as publish as builds

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
# release == production

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# copy from /app/publish in publish image and put it in curr dir
ENTRYPOINT ["dotnet", "Catalog.API.dll"]