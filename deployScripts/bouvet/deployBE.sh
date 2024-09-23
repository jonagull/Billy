cd /Users/jonathan/01_code/Billy/backend/Billy-BE
pwd
echo $$
docker -H ssh://root@boi build -t bouvet-boi .
ssh root@boi "docker stop billy-bouvet; 
docker container rm billy-bouvet; 
docker run --name billy-bouvet -d -p 1338:80 -v /root/bouvet/data.db:/app/data.db bouvet-boi"
echo "Docker deploy complete"


