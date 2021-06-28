#!/usr/bin/env bash
cd $PWD

export COMPOSE_DOCKER_CLI_BUILD=0

docker-compose down --volumes --remove-orphans

docker-compose build --force-rm --parallel --quiet
docker-compose up --detach --force-recreate

sleep 60

docker ps -a --format "table {{.Names}}\t{{.ID}}\t{{.Image}}\t{{.Status}}\t{{.Ports}}"

export AWS_ACCESS_KEY_ID=xxx
export AWS_SECRET_ACCESS_KEY=xxx
export DEFAULT_REGION=eu-west-1

echo "Creating Table..."
aws --endpoint-url=http://localhost:4566 dynamodb create-table \
    --table-name TodoItems \
    --attribute-definitions \
		AttributeName=Id,AttributeType=N \
		AttributeName=Type,AttributeType=S \
    --key-schema \
		AttributeName=Id,KeyType=HASH \
		AttributeName=Type,KeyType=RANGE \
    --provisioned-throughput \
		ReadCapacityUnits=5,WriteCapacityUnits=5 &> /dev/null
echo "Done Creating Table."