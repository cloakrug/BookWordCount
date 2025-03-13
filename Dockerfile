# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /BookWordCount
EXPOSE 80
EXPOSE 443
COPY . ./
COPY ["BookWordCount.csproj", "./"]
RUN dotnet restore "./BookWordCount.csproj"


# Build client
FROM node:latest as clientBuild
WORKDIR /ClientAppProd
COPY ClientApp/ .
RUN npm install
RUN npm run build

FROM base as build
RUN dotnet publish -c Release -o /BookWordCount/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0@latest AS final
WORKDIR /BookWordCount
COPY --from=build /BookWordCount/publish .
ENTRYPOINT ["dotnet", "BookWordCount.dll"]