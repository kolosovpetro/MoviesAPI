#!/bin/bash
sub=$1

random=$(shuf -i 0-50 -n1)

prefix="aks${random}"
echo "Prefix is ${prefix}"

aksName="aks-$prefix"
echo "Cluster name is ${aksName}"

location="northeurope"
echo "Location is ${location}"

# sub="f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
rgName="rg-$aksName"
acrName="k8smoviesacr${random}"

if [ -z "$sub" ]
then
      sub="f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
else
      echo "\$var is NOT empty"
fi

# Create resource group
az group create --location $location --subscription $sub --name $rgName

# Create ACR
az acr create --resource-group $rgName --name $acrName --sku "Basic"

# create AKS cluster
az aks create --generate-ssh-keys --subscription $sub \
    --node-count 3 --resource-group $rgName \
    --name $aksName --attach-acr $acrName --tier free
