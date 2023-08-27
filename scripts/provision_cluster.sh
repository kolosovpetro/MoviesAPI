#!/bin/bash
sub=$1

random=$(shuf -i 0-50 -n1)

prefix="aks${random}"
echo "Prefix is ${prefix}"

aksName="aks-$prefix"
echo "Cluster name is ${aksName}"

rgName="rg-$aksName"
echo "Resource group name is ${rgName}"

location="northeurope"
echo "Location is ${location}"

acrName="k8smoviesacr${random}"
echo "ACR name is ${acrName}"

defaultSub="f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

if [ -z "$sub" ]
then
      echo "Subscription is empty, using default: ${defaultSub}"
      sub="f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
else
      echo "Subscription id is ${sub}"
fi

# Create resource group
echo "Creating resource group ${rgName} in ${location}..."
az group create --location $location --subscription $sub --name $rgName

# Create ACR
echo "Creating ACR ${acrName} in ${rgName}..."
az acr create --resource-group $rgName --name $acrName --sku "Basic"

# create AKS cluster
echo "Creating AKS cluster ${aksName} in ${rgName}..."
az aks create --generate-ssh-keys --subscription $sub \
    --node-count 3 --resource-group $rgName \
    --name $aksName --attach-acr $acrName --tier free

# Importing credentials
echo "Importing cluster credentials..."
az aks get-credentials --resource-group $rgName --name $aksName --subscription $sub
