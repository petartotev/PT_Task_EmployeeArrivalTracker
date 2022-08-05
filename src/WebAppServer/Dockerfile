FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAppServer.Api.dll", "--environment=Development"]

########################################

# cd ../WebAppServer
# docker build -t <docker-hub-id>/webappserverimage .
# docker login
# docker push <docker-hub-id>/webappserverimage

# docker run -p 5168:80 --name webappservercontainer -d <docker-hub-id>/webappserverimage

# http://localhost:5168/api/arrivals