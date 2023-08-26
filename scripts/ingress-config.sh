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