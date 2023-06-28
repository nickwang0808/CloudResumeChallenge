#!/bin/bash

# Define container and database details
container_name="postgres"
database_name="postgres"
port="5432"
password="postgres"

# Check if the container exists
if [ "$(docker ps -aq -f name=$container_name)" ]; then
    # Stop the existing container
    docker stop $container_name

    # Remove the existing container
    docker rm $container_name
fi

# Run the PostgreSQL container
docker run -d --name $container_name -p $port:5432 -e POSTGRES_PASSWORD=$password postgres

# Wait for the container to initialize
echo "Waiting for the container to initialize..."
sleep 5

# Create the database
docker exec -it $container_name psql -U postgres -c "CREATE DATABASE $database_name"

echo "PostgreSQL database container has been created."
