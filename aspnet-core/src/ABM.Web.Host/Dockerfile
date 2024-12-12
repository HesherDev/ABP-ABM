FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/ABM.Web.Host/ABM.Web.Host.csproj", "src/ABM.Web.Host/"]
COPY ["src/ABM.Web.Core/ABM.Web.Core.csproj", "src/ABM.Web.Core/"]
COPY ["src/ABM.Application/ABM.Application.csproj", "src/ABM.Application/"]
COPY ["src/ABM.Core/ABM.Core.csproj", "src/ABM.Core/"]
COPY ["src/ABM.EntityFrameworkCore/ABM.EntityFrameworkCore.csproj", "src/ABM.EntityFrameworkCore/"]
WORKDIR "/src/src/ABM.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ABM.Web.Host", "src/ABM.Web.Host"]
COPY ["src/ABM.Web.Core", "src/ABM.Web.Core"]
COPY ["src/ABM.Application", "src/ABM.Application"]
COPY ["src/ABM.Core", "src/ABM.Core"]
COPY ["src/ABM.EntityFrameworkCore", "src/ABM.EntityFrameworkCore"]
WORKDIR "/src/src/ABM.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ABM.Web.Host.dll"]
