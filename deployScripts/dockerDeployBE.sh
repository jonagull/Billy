cd /Users/boien/code/POOL/billy/backend/Billy-BE
pwd
echo $$
docker -H ssh://root@boi build -t boi .
ssh root@boi "docker stop billy-container; docker container rm billy-container; docker run --name billy-container -d -p 1337:80 -v /root/projects/billydatabase.db:/app/data.db boi"
echo "Docker deploy complete"

