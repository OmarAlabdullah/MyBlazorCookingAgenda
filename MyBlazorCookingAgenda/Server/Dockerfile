FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyBlazorCookingAgenda/Server/MyBlazorCookingAgenda.Server.csproj", "MyBlazorCookingAgenda/Server/"]
COPY ["MyBlazorCookingAgenda/Client/MyBlazorCookingAgenda.Client.csproj", "MyBlazorCookingAgenda/Client/"]
COPY ["MyBlazorCookingAgenda/Shared/MyBlazorCookingAgenda.Shared.csproj", "MyBlazorCookingAgenda/Shared/"]
RUN dotnet restore "MyBlazorCookingAgenda/Server/MyBlazorCookingAgenda.Server.csproj"
COPY . .
WORKDIR "/src/MyBlazorCookingAgenda/Server"
RUN dotnet build "MyBlazorCookingAgenda.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyBlazorCookingAgenda.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlazorCookingAgenda.Server.dll"]
