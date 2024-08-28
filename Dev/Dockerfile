FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /API
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /API/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS running
WORKDIR /API
COPY --from=build /API/out ./
EXPOSE 80
EXPOSE 443
ENTRYPOINT [ "dotnet", "./API.dll" ]

# Dockerfile for API and dependencies.