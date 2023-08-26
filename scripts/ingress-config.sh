az aks show --resource-group "aks-k8s-rg" --name "aks-k8s" --query nodeResourceGroup -o tsv
nodeRg=$(az aks show --resource-group "aks-k8s-rg" --name "aks-k8s" --query nodeResourceGroup -o tsv)

az network public-ip create --resource-group $nodeRg --name "ingress-public-ip" --sku Standard --allocation-method static --query publicIp.ipAddress -o tsv
publicIp=$(az network public-ip create --resource-group $nodeRg --name "ingress-public-ip" --sku Standard --allocation-method static --query publicIp.ipAddress -o tsv)

## install ingress

## install ingress via Helm
DNS_LABEL="aksmovies.devtest.team"
NAMESPACE="ingress-basic"
STATIC_IP=4.210.95.219

helm repo add nginx-stable https://helm.nginx.com/stable
helm repo update
kubectl create namespace $NAMESPACE
helm install ingress-nginx nginx-stable/nginx-ingress --namespace $NAMESPACE

helm upgrade ingress-nginx nginx-stable/nginx-ingress \
  --namespace $NAMESPACE \
  --set controller.service.annotations."service\.beta\.kubernetes\.io/azure-dns-label-name"=$DNS_LABEL \
  --set controller.service.loadBalancerIP=$STATIC_IP \
  --set controller.service.annotations."service\.beta\.kubernetes\.io/azure-load-balancer-health-probe-request-path"=/healthz

# New try

helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo update

NAMESPACE=ingress-basic
helm install ingress-nginx ingress-nginx/ingress-nginx \
  --create-namespace \
  --namespace $NAMESPACE \
  --set controller.service.annotations."service\.beta\.kubernetes\.io/azure-load-balancer-health-probe-request-path"=/healthz

# Add Acr images

REGISTRY_NAME="k8smoviesacr01" \
SOURCE_REGISTRY=registry.k8s.io \
CONTROLLER_IMAGE=ingress-nginx/controller \
CONTROLLER_TAG=v1.2.1 \
PATCH_IMAGE=ingress-nginx/kube-webhook-certgen \
PATCH_TAG=v1.1.1 \
DEFAULTBACKEND_IMAGE=defaultbackend-amd64 \
DEFAULTBACKEND_TAG=1.5
az acr import --name $REGISTRY_NAME --source $SOURCE_REGISTRY/$CONTROLLER_IMAGE:$CONTROLLER_TAG --image $CONTROLLER_IMAGE:$CONTROLLER_TAG
az acr import --name $REGISTRY_NAME --source $SOURCE_REGISTRY/$PATCH_IMAGE:$PATCH_TAG --image $PATCH_IMAGE:$PATCH_TAG
az acr import --name $REGISTRY_NAME --source $SOURCE_REGISTRY/$DEFAULTBACKEND_IMAGE:$DEFAULTBACKEND_TAG --image $DEFAULTBACKEND_IMAGE:$DEFAULTBACKEND_TAG