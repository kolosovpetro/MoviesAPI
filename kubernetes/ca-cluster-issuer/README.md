## Link

- https://github.com/azuredevcollege/trainingdays/blob/master/day7/challenges/bonus-1.md

## Commands

- kubectl create namespace cert-manager
- helm repo add jetstack https://charts.jetstack.io
- helm repo update
- helm upgrade -i -f cert-manager-values.yaml --namespace cert-manager cert-manager jetstack/cert-manager
- kubectl apply -f letsencrypt-prod-cluster-issuer.yaml
