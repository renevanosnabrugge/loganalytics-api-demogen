FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
COPY ["Demo.CustomLogs/Demo.CustomLogs.csproj", "Demo.CustomLogs/"]
RUN dotnet restore "Demo.CustomLogs/Demo.CustomLogs.csproj"
COPY . .
RUN dotnet build "Demo.CustomLogs/Demo.CustomLogs.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.CustomLogs/Demo.CustomLogs.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "Demo.CustomLogs.dll"]