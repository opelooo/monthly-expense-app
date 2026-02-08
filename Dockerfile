# Stage 1: Build (Requires the SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy csproj and restore
COPY ["OpenExpenseApp.csproj", "./"]
RUN dotnet restore "OpenExpenseApp.csproj"

# Copy all files and publish
COPY . .
RUN dotnet publish "OpenExpenseApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime (Requires only the ASP.NET runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# Install ICU for globalization support in Alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "OpenExpenseApp.dll"]
