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
RUN dotnet publish -c Release -o /BookWordCount/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
EXPOSE 443
WORKDIR /BookWordCount
COPY --from=build /BookWordCount/publish .
ENTRYPOINT ["dotnet", "BookWordCount.dll"]