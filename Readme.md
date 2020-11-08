# ApiChallange
A Web API wrote in .Net Core 3.1 that return a pokemon's description written in Shakespeare's style.

# Requirements
Docker https://www.docker.com/

HTTP client (httpie/Postman)

.Net Core 3.1 (only build and test) https://dotnet.microsoft.com/download/dotnet-core/3.1

# Run Container
Container image is available in my public Docker Hub repository.

To run the container execute the following command where {yourPortNumber} is the port you choose to expose the api
```
docker run -it --rm -p {yourPortNumber}:80 mancio23/apichallange:latest
```
Usage example:
``` 
docker run -it --rm -p 8080:80 mancio23/apichallange:latest
```

# Invoke
When the Container is running use an HTTP client to invoke the api.

Using httpie:
``` 
http http://localhost:{yourPortNumber}/pokemon/charizard
```

# Build
```
dotnet build
```

# Test
```
dotnet test
```
