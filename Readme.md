# Todo API

## Overview

Todo API allows the client to create, read, update and delete todo items.

### Architecture

The API is build using DDD and CQRS.

For Persistence, uses DynamoDB

For local development, uses LocalStack

HealthCheck endpoints.

### Dependencies

1. [MediatR](https://github.com/jbogard/MediatR)
1. [Fluent Assertions](https://fluentassertions.com/)
1. [Fluent Validation](https://fluentvalidation.net/)
1. [StronglyTypedId](https://github.com/andrewlock/StronglyTypedId)
1. [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions)
1. [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
1. [LocalStack](https://localstack.cloud/)

### Build

`dotnet build build.proj`

### Tests

`dotnet test`

## Using Docker

### Build App

`docker build -t todoapi .`

`docker run -d -p 8080:80 --name todoapiapp todoapi`

### Remove App

`docker rm todoapiapp`

## Using Docker Compose

`./run.sh`

`./stop.sh`
