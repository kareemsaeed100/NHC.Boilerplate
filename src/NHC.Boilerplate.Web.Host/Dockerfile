FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/NHC.Boilerplate.Web.Host/NHC.Boilerplate.Web.Host.csproj", "src/NHC.Boilerplate.Web.Host/"]
COPY ["src/NHC.Boilerplate.Web.Core/NHC.Boilerplate.Web.Core.csproj", "src/NHC.Boilerplate.Web.Core/"]
COPY ["src/NHC.Boilerplate.Application/NHC.Boilerplate.Application.csproj", "src/NHC.Boilerplate.Application/"]
COPY ["src/NHC.Boilerplate.Core/NHC.Boilerplate.Core.csproj", "src/NHC.Boilerplate.Core/"]
COPY ["src/NHC.Boilerplate.EntityFrameworkCore/NHC.Boilerplate.EntityFrameworkCore.csproj", "src/NHC.Boilerplate.EntityFrameworkCore/"]
WORKDIR "/src/src/NHC.Boilerplate.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/NHC.Boilerplate.Web.Host", "src/NHC.Boilerplate.Web.Host"]
COPY ["src/NHC.Boilerplate.Web.Core", "src/NHC.Boilerplate.Web.Core"]
COPY ["src/NHC.Boilerplate.Application", "src/NHC.Boilerplate.Application"]
COPY ["src/NHC.Boilerplate.Core", "src/NHC.Boilerplate.Core"]
COPY ["src/NHC.Boilerplate.EntityFrameworkCore", "src/NHC.Boilerplate.EntityFrameworkCore"]
WORKDIR "/src/src/NHC.Boilerplate.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "NHC.Boilerplate.Web.Host.dll"]
