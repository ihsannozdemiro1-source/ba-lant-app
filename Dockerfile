FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BaglantiApp/BaglantiApp.csproj"
RUN dotnet publish "BaglantiApp/BaglantiApp.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BaglantiApp.dll"]
