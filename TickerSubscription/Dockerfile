FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TickerSubscription/TickerSubscription.csproj", "TickerSubscription/"]
RUN dotnet restore "TickerSubscription/TickerSubscription.csproj"
COPY . .
WORKDIR "/src/TickerSubscription"
RUN dotnet build "TickerSubscription.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TickerSubscription.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TickerSubscription.dll"]