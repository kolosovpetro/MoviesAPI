## Link

- https://github.com/azuredevcollege/trainingdays/blob/master/day7/challenges/challenge-1.md

# Commands

- kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.6.0/aio/deploy/recommended.yaml
- kubectl apply -f dashboard-admin.yaml
- kubectl -n kubernetes-dashboard create token admin-user
- kubectl proxy
- kubectl -n kubernetes-dashboard get secret
- kubectl -n kubernetes-dashboard describe secret admin-user-token-smw2j
- kubectl proxy
- http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/