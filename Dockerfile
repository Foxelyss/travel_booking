FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 

WORKDIR /source

COPY *.sln .
COPY /src/*.csproj ./src/
COPY /src /src

WORKDIR /src

RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0

RUN apt-get update && apt-get install -y curl

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development 
WORKDIR /app

COPY --from=build /app ./

STOPSIGNAL SIGINT 
ENTRYPOINT ["dotnet", "TravelBooking.dll"]