function list_child_processes () {
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 66814;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 66814 > /dev/null;
done;

for child in $(list_child_processes 66816);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/donetianpetkov/Documents/GitHub/HouseManagementApp/PersonalManagementApp/bin/Debug/net6.0/17044089336840cc9cbd9bcf1d330c57.sh;
