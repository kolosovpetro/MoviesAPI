# connect to Azure via Azure CLI
az login

# create service principal
az ad sp create-for-rbac --role contributor --scopes "/subscriptions/f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --name "Movies_AKS_SP" --sdk-auth

# varaibles
prefix="k01" \
k8sName="aks-$prefix" \
location="northeurope" \
sub="f32f6566-8fa0-4198-9c91-a3b8ac69e89a" \
rgName="rg-$k8sName" \
acrName="k8smoviesacr01"


# Create resource group
az group create --location $location --subscription $sub --name $rgName

# Create ACR
az acr create --resource-group $rgName --name $acrName --sku "Basic"

# create AKS cluster
az aks create --generate-ssh-keys --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a" --node-count 3 --resource-group "aks-k8s-rg" --name "aks-k8s" --tier free
az aks create --generate-ssh-keys --subscription $sub \
    --node-count 3 --resource-group $rgName \
    --name $k8sName --attach-acr $acrName --tier free

# Update exising cluster with ACR
az aks update -n "aks-k8s" -g "aks-k8s-rg" --attach-acr "aksk8sacr"
az aks update -n "cars-aks-k8s-d01" -g "cars-island-d01" --attach-acr "acrcarsislandd01"

# connect to cluster
az aks get-credentials --resource-group $rgName --name $k8sName --subscription $sub
az aks get-credentials --resource-group "rg-aks-aks42" --name "aks-aks42" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks get-credentials --resource-group "cars-island-d01" --name "cars-aks-k8s-d01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks get-credentials --resource-group "cars-island-c01" --name "cars-aks-k8s-c01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

# get access to Dashboard
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Open Dashboard
az aks browse --resource-group "aks-k8s-rg" --name "aks-k8s" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks browse --resource-group "cars-island-d01" --name "cars-aks-k8s-d01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"
az aks browse --resource-group "cars-island-c01" --name "cars-aks-k8s-c01" --subscription "f32f6566-8fa0-4198-9c91-a3b8ac69e89a"

# Create config map
kubectl create configmap "movies-configmap" --from-file=movies-configmap.yaml
kubectl get configmaps
kubectl get namespace
kubectl describe configmap "movies-configmap"
kubectl describe service "movies-api-service"
kubectl get pod -o wide
kubectl apply -f ./api-deployment.azure.yaml
kubectl apply -f ./api-deployment.azure.yaml --namespace app
kubectl apply -f ./app-deployment.azure.yaml
kubectl apply -f ./mssql-deployment/ --namespace app
kubectl apply -f ./api-deployment/
kubectl get pods
kubectl delete -f cars-configmap.yaml -n default
kubectl delete -f ./api-deployment.azure.yaml -n default
kubectl delete -f ./app-deployment.azure.yaml -n default
kubectl create -f ./cars-configmap.yaml
kubectl create -f ./api-deployment.azure.yaml
kubectl logs movies-api-deployment

## check endpoints
kubectl get endpoints
kubectl describe service "cars-api-service"

curl -v http://20.191.53.75/api/Car/all