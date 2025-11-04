#!/bin/sh

port=$(lsof -t -i :5000 | head -n 1 | grep -v "PID")
                    
if [ -z "$port" ]; 
then
  echo "Port 5000 is not in use"
else
  echo "Port 5000 is in use"
  kill -9 "$port"
fi