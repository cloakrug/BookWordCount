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
COPY --from=clientBuild /ClientAppProd/dist /BookWordCount/wwwroot
RUN dotnet publish -c Release -o /BookWordCount/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /BookWordCount
COPY --from=build /BookWordCount/publish .
ENTRYPOINT ["dotnet", "BookWordCount.dll"]