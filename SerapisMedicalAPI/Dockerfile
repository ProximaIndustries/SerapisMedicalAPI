#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#WORKDIR /app

#COPY . .

#CMD ASPNETCORE_URLS=http://*:$PORT dotnet SerapisMedicalAPI.dll

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /build

#RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
#RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY ./*.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /build
RUN dotnet publish -c release -o published --no-cache

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /build/published ./
ENTRYPOINT ["dotnet", "SerapisMedicalAPI.dll"]