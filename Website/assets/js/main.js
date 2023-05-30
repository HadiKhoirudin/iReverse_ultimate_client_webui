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
var QlMGPTGridtbody = document.getElementById("QlMGPTGridtbody");
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
	 
	 
  if (data.hasOwnProperty('QlMGPTGrid'))
  {
	  if (data.hasOwnProperty('QlMGPTGrid') && data.message === ">clear<") {
		 QlMGPTGridtbody.innerHTML = "";
	  } else {
		  var checkbox;
		  var checked = data.hasOwnProperty('checked') ? data.checked : '';
		  if (checked){ checkbox ='checked'; } else { checkbox =''; }
		  var partition = data.hasOwnProperty('partition') ? data.partition : '';
		  var offset = data.hasOwnProperty('offset') ? data.offset : '';
		  var length = data.hasOwnProperty('length') ? data.length : '';
		  var custom = data.hasOwnProperty('custom') ? data.custom : '';
		  var flags = data.hasOwnProperty('flags') ? data.flags : '';
		  var UUID = data.hasOwnProperty('UUID') ? data.UUID : '';
		  
		  QlMGPTGridtbody.innerHTML += '<tr class="tbrowsitems">' +
											'<td><input type="checkbox" name="cbpartition" '+ checkbox +'/></td>' +
											'<td>'+ partition +'</td>' +
											'<td>'+ offset +'</td>' +
											'<td>'+ length +'</td>' +
											'<td>'+ custom +'</td>' +
											'<td>'+ flags +'</td>' +
											'<td>'+ UUID +'</td>' +
									   '</tr>';
		  $(".tbrowsitems").last()[0].scrollIntoView({ behavior: 'smooth' });
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


$('.btn.btn-danger.exec').click(function(e) {
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


$('#DataView').on('click', '#select_all', function () {
	var elms = document.getElementsByName('cbpartition');
	if ($('#select_all:checked').val() === 'on')
	{
	for (var i = 0; i < elms.length; i++) { elms[i].setAttribute("checked", ""); }
	} else {
	for (var i = 0; i < elms.length; i++) { elms[i].removeAttribute("checked"); }
	}
});
$(function() {
  const $headers = $("#DataView thead tr th"); 
  $("#btnGet").on("click", function() {
    const data = [];
    $("#DataView tbody input[type=checkbox]:checked").each(function(index) {
      $row = $(this).closest("tr");
      $cells = $row.find("td"); // or use some selector to ignore the first cell
      data[index] = {};
      $cells.each(function(cellIndex) {
        if (cellIndex>0) data[index][$headers.eq(cellIndex).html()] = $(this).html();
      });
    });
    console.log(JSON.stringify(data));
  });

});

$('#QlMGPTGrid').on('click', '#select_all', function () {
	var elms = document.getElementsByName('cbpartition');
	if ($('#select_all:checked').val() === 'on')
	{
	for (var i = 0; i < elms.length; i++) { elms[i].setAttribute("checked", ""); }
	} else {
	for (var i = 0; i < elms.length; i++) { elms[i].removeAttribute("checked"); }
	}
});
$(function() {
  const $headers = $("#QlMGPTGrid thead tr th"); 
  $("#btnGet").on("click", function() {
    const data = [];
    $("#QlMGPTGrid tbody input[type=checkbox]:checked").each(function(index) {
      $row = $(this).closest("tr");
      $cells = $row.find("td"); // or use some selector to ignore the first cell
      data[index] = {};
      $cells.each(function(cellIndex) {
        if (cellIndex>0) data[index][$headers.eq(cellIndex).html()] = $(this).html();
      });
    });
    console.log(JSON.stringify(data));
  });

});
