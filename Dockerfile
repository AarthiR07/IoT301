<<<<<<< HEAD
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7011

ENV ASPNETCORE_URLS=http://+:7011

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IoTProject301.csproj", "./"]
RUN dotnet restore "IoTProject301.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IoTProject301.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoTProject301.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoTProject301.dll"]
=======
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7011

ENV ASPNETCORE_URLS=http://+:7011

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IoTProject301.csproj", "./"]
RUN dotnet restore "IoTProject301.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IoTProject301.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoTProject301.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoTProject301.dll"]
>>>>>>> dd1f05fb1117b6428a272dc81c80cb304ea694b1
