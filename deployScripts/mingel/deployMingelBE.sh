#cd /Users/jonathan/01_code/Billy/backend/Billy-BE
cd /Users/jonathan.nodland.gulliksen/code/Billy/backend/Billy-BE
pwd
echo $$
docker -H ssh://root@boi build -t mingel-boi .
ssh root@boi "docker stop billy-mingel; 
docker container rm billy-mingel; 
docker run --name billy-mingel -d -p 1339:80 -v /root/mingel/data.db:/app/data.db mingel-boi"
echo "Docker deploy complete"


