# Use the official .NET SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o out

# Use the official runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the published output from the build image
COPY --from=build /app/out .

# Expose the port that the application will run on
EXPOSE 5000
ENV ASPNET_HTTP_PORTS=5000

# Set the entry point for the application
ENTRYPOINT ["dotnet", "BrazilSurvival.BackEnd.dll"]
