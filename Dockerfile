# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /BookWordCount
COPY ["BookWordCount.csproj", "./"]
RUN dotnet restore "./BookWordCount.csproj"

COPY . ./

# Build client
FROM node:latest as clientBuild
WORKDIR /ClientAppProd
COPY ClientApp/ .
RUN npm install
RUN npm run build

FROM base as build
RUN dotnet publish -c Release -o /BookWordCount/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
WORKDIR /BookWordCount
COPY --from=build /BookWordCount/publish .
ENTRYPOINT ["dotnet", "BookWordCount.dll"]