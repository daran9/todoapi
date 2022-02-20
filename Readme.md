# Todo API

## Overview

Todo API allows the client to create, read, update and delete todo items.

### Architecture

The API is build using DDD and CQRS.

For Persistence, uses DynamoDB

For local development, uses LocalStack

HealthCheck endpoint: http://<HOSTNAME:PORT>/health

API endpoint: http://<HOSTNAME:PORT>/api/todo

Swagger doc endpoint: http://<HOSTNAME:PORT>/swagger/index.html

### Dependencies

1. [MediatR](https://github.com/jbogard/MediatR)
1. [Fluent Assertions](https://fluentassertions.com/)
1. [Fluent Validation](https://fluentvalidation.net/)
1. [StronglyTypedId](https://github.com/andrewlock/StronglyTypedId)
1. [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions)
1. [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
1. [LocalStack](https://localstack.cloud/)

## Execution

### Build

To build the app and the test run :

`dotnet build build.proj`

### Tests

For Integration Tests, run : `dotnet test .\test\TodoAPI.Test.Integration\TodoAPI.Test.Integration.csproj`

For Unit Tests, run: `dotnet test .\test\TodoAPI.Test.Unit\TodoAPI.Test.Unit.csproj`

### Run

Setup AWS DynamoDB table and login with AWS CLI to save the access key and secret to credentials file

To run the project : `dotnet run --project .\src\TodoApi\TodoApi.csproj`

## Using Docker

### Build App

To build the app with docker, run

`docker build -t todoapi .`

`docker run -d -p 8080:80 --name todoapiapp todoapi`

### Remove App

To remove the app, run:

`docker rm todoapiapp`

## Using Docker Compose

To run the stack:

`./run.sh`

To stop the stack:

`./stop.sh`
