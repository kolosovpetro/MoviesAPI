#!/bin/bash

SERVICE_NAME="movies"

if systemctl -q is-active "$SERVICE_NAME"; then
    echo "Service $SERVICE_NAME is running. Restarting..."
    sudo systemctl daemon-reload
    sudo systemctl restart "$SERVICE_NAME"
    sudo systemctl status "$SERVICE_NAME"
else
    echo "Service $SERVICE_NAME doesn't exist. Starting..."
    sudo systemctl daemon-reload
    sudo systemctl start "$SERVICE_NAME"
    sudo systemctl enable "$SERVICE_NAME"
    sudo systemctl status "$SERVICE_NAME"
fi
