domain="devtest.team" \
email="kolosovp94@gmail.com" \
server="https://acme-v02.api.letsencrypt.org/directory"

certbot certonly --manual --preferred-challenges=dns --email $email \
    --server $server -d "*.${domain}" -d "$domain"

nslookup -type=txt _acme-challenge.$domain

# From folder C:\Certbot\live\mangomesenger.company in CMD as Admin
type fullchain.pem privkey.pem > bundle.pem

openssl pkcs12 -export -out "certificate_combined.pfx" -inkey "privkey.pem" -in "cert.pem" -certfile bundle.pem