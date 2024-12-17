## Build Container
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

## Runtime Container
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# Install cultures
RUN apk add --no-cache \
   icu-data-full \
   icu-libs

ENV ASPNETCORE_URLS=http://*:80

WORKDIR /app
COPY --from=builder /app/out ./

# Expose the port your application will run on
EXPOSE 80

ENTRYPOINT ["dotnet", "Experiment.PO.dll"]