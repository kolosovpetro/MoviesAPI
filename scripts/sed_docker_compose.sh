#!/bin/bash

if [ -z "$1" ]; then
  echo "Please provide the path to the docker-compose.yml file"
  exit 1
fi

if [ -z "$2" ]; then
  echo "Please provide version number"
  exit 1
fi

sed -i "s|__TAG__|$2|" "$1"

echo "Docker compose file updated successfully"
echo "Result: $(cat "$1")"
