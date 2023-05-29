var channel_me = 'private-channel-ireverse';
var	event_me = 'client-User-Web';
var	event_to = 'client-ireverse-001';

var app_key = '743996c65a2c3b504344';
var pusher = new Pusher(app_key, {
	cluster: 'ap1',
	forceTLS: false
});

channel = pusher.subscribe(channel_me);
channel.bind(event_me, function (data) {

console.log(data);
var logContainer = document.getElementById("logContainer");
var Progressbar1 = document.getElementById("Progressbar1");
var Progressbar2 = document.getElementById("Progressbar2");
  if (data.hasOwnProperty('message'))
  {
	  if (data.hasOwnProperty('message') && data.message === ">clear<") {
		 logContainer.innerHTML = ""; // Mengosongkan logContainer jika clear:true
	  } else {
			 var logText = data.hasOwnProperty('message') ? data.message : '';
			 var logClass = data.hasOwnProperty('color') ? data.color : '';
			 if (data.hasOwnProperty('newline') && data.newline) {
				logContainer.innerHTML += '<span class="' + logClass + ' richlogs">' + logText + '</span> <br>';
				$(".richlogs").last()[0].scrollIntoView({ behavior: 'smooth' });
			 } else {
				logContainer.innerHTML += '<span class="' + logClass + ' richlogs">' + logText + '</span>';
			 }
		 }
	 }
	 
	 
  if (data.hasOwnProperty('progressbar1'))
  {
	Progressbar1.style.width = data.progressbar1 + '%';
	Progressbar1.innerHTML = data.progressbar1 + '%';
  }
  
  if (data.hasOwnProperty('progressbar2'))
  {
	Progressbar2.style.width = data.progressbar2 + '%';
	Progressbar2.innerHTML = data.progressbar2 + '%';
  }
  
});

$('#formexec').submit(function (e) {
	e.preventDefault();
	var dataform = $('#formexec').serialize();
          $.ajax({
              url : "trigger.php",
              type: "POST",
              data: dataform,
              success: function(data)
              { 
				console.log('Ok!');
			  },
              error: function (jqXHR, textStatus, errorThrown)
              {
				  
                  alert("Mohon maaf! Sepertinya ada masalah dengan server!");
      
              }
          });

});