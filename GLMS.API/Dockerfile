# Stage 1: Build runtime environment
FROM ://microsoft.com AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Compile and restore dependencies efficiently
FROM ://microsoft.com AS build
WORKDIR /src
COPY ["GLMS.API/GLMS.API.csproj", "GLMS.API/"]
RUN dotnet restore "GLMS.API/GLMS.API.csproj"
COPY . .
WORKDIR "/src/GLMS.API"
RUN dotnet build "GLMS.API.csproj" -c Release -o /app/build

# Stage 3: Publish application artifacts
FROM build AS publish
RUN dotnet publish "GLMS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final minimal secure image layout
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GLMS.API.dll"]
