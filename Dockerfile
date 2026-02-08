# 1. Use the build machine's native platform for the SDK stage (fast)
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG TARGETARCH
WORKDIR /src

RUN apk add --no-cache icu-libs icu-data-full

# Copy and Restore specifically for the target architecture (ARM64)
COPY ["OpenExpenseApp.csproj", "./"]
RUN dotnet restore "OpenExpenseApp.csproj" -a $TARGETARCH

# Copy all files and Publish for ARM64
COPY . .
RUN dotnet publish "OpenExpenseApp.csproj" -c Release -o /app/publish -a $TARGETARCH --self-contained false

# 2. Final stage: Use the actual ARM64 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

COPY --from=build /usr/lib/libicu* /usr/lib/
COPY --from=build /usr/share/icu /usr/share/icu

COPY --from=build /app/publish .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "OpenExpenseApp.dll"]
