FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["FirstDemo.Web/*.csproj", "FirstDemo.Web/"]
COPY ["FirstDemo.Data/*.csproj", "FirstDemo.Data/"]
COPY ["FirstDemo.Common/*.csproj", "FirstDemo.Common/"]
COPY ["FirstDemo.Membership/*.csproj", "FirstDemo.Membership/"]
COPY ["FirstDemo.Training/*.csproj", "FirstDemo.Training/"]
COPY ["FirstDemo.Utility/*.csproj", "FirstDemo.Utility/"]
RUN dotnet restore "FirstDemo.Web/FirstDemo.Web.csproj"
COPY . .
WORKDIR "/src/FirstDemo.Web"
RUN dotnet build "FirstDemo.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FirstDemo.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FirstDemo.Web.dll"]