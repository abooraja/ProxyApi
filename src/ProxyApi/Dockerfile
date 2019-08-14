FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/ProxyApi/ProxyApi.csproj src/ProxyApi/
RUN dotnet restore src/ProxyApi/ProxyApi.csproj
COPY . .
WORKDIR /src/src/ProxyApi
RUN dotnet build ProxyApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ProxyApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProxyApi.dll"]
