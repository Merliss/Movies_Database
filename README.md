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

Another important case is https certificate on Linux machine. In my case (Ubuntu 20.04) you need to generate certificate and add it to the /https folder in main directory of project (where the docker-compose.yaml file is).
You must run these commands in your project directory:
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust
```
Then copy this .pfx certificate to https folder you created. The command in Dockerfile will copy this file to suitable directory in container.

The  database update must be prepared by user (to fill the database). You need to change settings in code that connect to database (MovieDbContext class, commented line of code) by using local appsetings.json file and run:
```
dotnet eff database update
```
Then revert back the changes in MovieDbContext and start containers by:
```
docker-compose up
```

Database schema:
![obraz](https://github.com/Merliss/Movies_Database/assets/62032793/9d747ad8-a00e-4f99-9b3a-21236bbc32d7)

