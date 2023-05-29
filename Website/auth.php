<?php
#echo '{ "auth": "1afff399e76480e04bfe:0c40bb0f16357b2a2cb85f2fb631ea75ff0df0a7c760b36b76e5839c83d7354as", "user_data": "USER_DATA" }';
  date_default_timezone_set('Asia/Jakarta');
  ini_set('date.timezone', 'Asia/Jakarta');
  require __DIR__ . '/vendor/autoload.php';

  #'YOUR_APP_ID';
  $app_id = '1604614';

  #'YOUR_APP_KEY';
  $app_key = '743996c65a2c3b504344';

  #'YOUR_APP_SECRET';
  $app_secret = '9317a9ca542aef9b4343';

  #'YOUR_APP_CLUSTER';
  $app_cluster = array(
    'cluster' => 'ap1',
    'useTLS' => true
  );

  $socket_id = "";
  $channel_name = "";
  
  if(isset($_POST['socket_id']))
  {
	$socket_id = $_POST['socket_id'];
	$channel_name = $_POST['channel_name'];
  }elseif(isset($_GET['socket_id']))
  {
	$socket_id = $_GET['socket_id'];
	$channel_name = $_GET['channel_name'];
  }
  
  $pusher = new Pusher\Pusher($app_key, $app_secret, $app_id, $app_cluster);
  $auth = $pusher->socket_auth($channel_name, $socket_id);
  echo $auth;
