# Todo API

## Using Docker

### Build App
docker build -t todoapi .

docker run -d -p 8080:80 --name todoapiapp todoapi

### Remove App
docker rm todoapiapp

## Using Docker Compose

./run.sh

./stop.sh