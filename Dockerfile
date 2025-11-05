FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 

WORKDIR /source

COPY *.sln .
COPY /src/*.csproj ./src/
COPY /src /src

WORKDIR /src

RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development 
WORKDIR /app

COPY --from=build /app ./
# ENTRYPOINT ["tail"]
# CMD ["-f","/dev/null"]
ENTRYPOINT ["dotnet", "TravelBooking.dll"]