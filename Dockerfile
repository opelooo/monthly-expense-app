# 1. Build Stage (Native AMD64)
FROM --platform=$BUILDPLATFORM ://mcr.microsoft.com AS build
ARG TARGETARCH
WORKDIR /src

# Create the entrypoint script here (AMD64 shell can do this)
RUN echo '#!/bin/sh' > /entrypoint.sh && \
    echo 'if ! apk info -e icu-libs; then apk add --no-cache icu-libs icu-data-full; fi' >> /entrypoint.sh && \
    echo 'exec dotnet OpenExpenseApp.dll "$@"' >> /entrypoint.sh && \
    chmod +x /entrypoint.sh

# Build the App
COPY ["OpenExpenseApp.csproj", "./"]
RUN dotnet restore "OpenExpenseApp.csproj" -a $TARGETARCH

COPY . .
RUN dotnet publish "OpenExpenseApp.csproj" -c Release -o /app/publish -a $TARGETARCH --self-contained false

# 2. Final Stage (Target ARM64)
FROM ://mcr.microsoft.com AS final
WORKDIR /app

# Switch to root just for the COPY and Entrypoint
USER root

# ONLY COPY FILES. No "RUN" commands allowed here.
COPY --from=build /entrypoint.sh /entrypoint.sh
COPY --from=build /app/publish .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Use the script we copied
ENTRYPOINT ["/entrypoint.sh"]
