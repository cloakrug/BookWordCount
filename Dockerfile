# Use an official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /BookWordCount
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /BookWordCount
COPY ["YourProject.csproj", "./"]
RUN dotnet restore "./YourProject.csproj"

COPY . .
RUN dotnet publish -c Release -o /BookWordCount/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /BookWordCount/publish .
ENTRYPOINT ["dotnet", "BookWordCount.dll"]