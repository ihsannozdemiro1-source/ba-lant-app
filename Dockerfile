FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BaglantiApp/BaglantiApp.csproj", "BaglantiApp/"]
RUN dotnet restore "BaglantiApp/BaglantiApp.csproj"
COPY . .
WORKDIR "/src/BaglantiApp"
RUN dotnet publish "BaglantiApp.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BaglantiApp.dll"]
