cd /Users/jonathan/01_code/Billy/frontend;
npm i -f;
npm run build;
scp -r build root@boi:/root/bouvet/Billy/;
#pm2 reload 9;

