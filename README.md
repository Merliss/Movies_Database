Movie DB WebAPI in C#.
This repository also contains the Dockerfile and docker-compose file that are used in on-server implementation.

If somehow "dotnet restore" didn't work use this commands inside docker container:
```
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.11

dotnet add package FluentValidation.AspNetCore --version 11.3.0

dotnet add package NLog --version 5.2.6

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.13

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.25

dotnet add package NLog.Web.AspNetCore --version 5.3.5
```
This should add packages if they weren't add before in the container build process.
