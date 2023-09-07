ssh root@boi "cd /root/projects/Billy/frontend; git fetch; git reset --hard master; npm i -f;  npm run build; pm2 reload 0; printf 'Deploy done'"

