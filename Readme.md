docker build -t todoapi .

docker run -d -p 8080:80 --name todoapiapp todoapi

# remove app
docker rm todoapiapp 