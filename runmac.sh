#!/bin/bash

if [ "$EUID" -ne 0 ]
  then echo "Please run as root"
  exit
fi

docker kill $(docker ps -q)
docker rm $(docker ps -a -q)
docker build . -t diaryapp
docker run -p 80:80 -p 8080:8080 -t diaryapp
