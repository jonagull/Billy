#ssh root@boi "cd /root/projects/Billy/frontend; git fet# ch; git reset --hard master; npm i -f;  npm run build; pm2 reload 0; printf 'Deploy done'"
ssh root@boi "cd /root/projects/Billy/frontend; git pull; npm i -f;  npm run build; pm2 reload 0; printf 'Deploy done'"

