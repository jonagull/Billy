#cd /Users/jonathan/01_code/Billy/frontend;
cd /Users/jonathan.nodland.gulliksen/code/Billy/frontend;
npm i -f;
npm run build;
scp -r build root@boi:/root/mingel/Billy/frontend;
#ssh root@boi "pm2 reload 9";
#pm2 reload 9;

