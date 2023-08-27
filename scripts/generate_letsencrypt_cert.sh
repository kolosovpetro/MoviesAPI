domain="devtest.team" \
email="kolosovp94@gmail.com" \
server="https://acme-v02.api.letsencrypt.org/directory"

sudo certbot certonly --manual --preferred-challenges=dns --email $email \
    --server $server -d "*.${domain}" -d "$domain"

nslookup -type=txt _acme-challenge.$domain

## Create AKS secret with SSL cert

sudo kubectl create secret tls tls-secret \
    --cert=Certs/devtest.team/fullchain.pem \
    --key=Certs/devtest.team/privkey.pem