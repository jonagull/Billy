ssh root@boi "cd /root/projects/Billy/frontend; git pull; npm run build; pm2 reload 0; printf 'Deploy done'"

