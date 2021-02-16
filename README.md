# Overview
Test task for HeidelbergCement AG position of the platform developer (Back-end) in Digital Products Team.

![Build&Test](https://github.com/m-yavuz/LogProxy/workflows/Build&Test/badge.svg)

![Build Docker](https://github.com/m-yavuz/LogProxy/workflows/Build%20Docker/badge.svg)
# How to build, test & run 

easliy you can build , test and run via batch files on the root folder.

## Build
- You can find **Build.bat** file on the root
- or you can use dotnet cli command

```bash
$ dotnet restore src
$ dotnet build src 
```

## Run
- You can find **run.bat** file on the root
- or you can use dotnet cli command
```bash
dotnet run -p src\LogProxy.API
```
## Test
- You can find **tests.bat** file on the root
- or you can use dotnet cli command
```bash
dotnet test -p src
```

# Docker
there is a docker file at the root, to run the docker:

```bash
docker build . --file Dockerfile
```

 