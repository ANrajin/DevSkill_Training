FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /MyApp
RUN apt-get update && apt-get install -y nodejs
COPY ["MyApp.Web/*.csproj", "MyApp.Web/"]
COPY ["MyApp.Data/*.csproj", "MyApp.Data/"]
COPY ["MyApp.Membership/*.csproj", "MyApp.Membership/"]
COPY ["MyApp.Training/*.csproj", "MyApp.Training/"]
COPY ["MyApp.Utility/*.csproj", "MyApp.Utility/"]
COPY ["UI.Library/*.csproj", "UI.Library/"]
RUN dotnet restore "MyApp.Web/MyApp.Web.csproj"
COPY . .
WORKDIR "/MyApp/MyApp.Web"
RUN dotnet build "MyApp.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyApp.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyApp.Web.dll"]
