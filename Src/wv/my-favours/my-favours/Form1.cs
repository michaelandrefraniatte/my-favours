using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using Microsoft.Web.WebView2.WinForms;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Wpf;
using WebView2 = Microsoft.Web.WebView2.WinForms.WebView2;
using System.Diagnostics;

namespace my_favours
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WebView2 webView21 = new WebView2();
        public void LoadHtml(string html)
        {
            webView21.NavigateToString(html);
        }
        public void OpenUrl(string url)
        {
            webView21.Source = new Uri(url);
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions("--disable-web-security", "--allow-file-access-from-files", "--allow-file-access");
            CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, null, options);
            await webView21.EnsureCoreWebView2Async(environment);
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets", "assets", CoreWebView2HostResourceAccessKind.DenyCors);
            webView21.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webView21.Source = new System.Uri("https://www.youtube.com/feed/subscriptions");
            webView21.CoreWebView2.AddHostObjectToScript("bridge", new Bridge());
            webView21.NavigationStarting += WebView21_NavigationStarting;
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
            webView21.LocationChanged += WebView21_LocationChanged;
            webView21.Dock = DockStyle.Fill;
            this.Controls.Add(webView21);
        }
        private void WebView21_LocationChanged(object sender, EventArgs e)
        {
        }
        private async void WebView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            string tempsavepath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(@"file:\", "").Replace(Process.GetCurrentProcess().ProcessName + ".exe", "").Replace(@"\", "/").Replace(@"//", "") + "tempsave";
            string savedstorage = "[]";
            if (File.Exists(tempsavepath))
            {
                using (StreamReader file = new StreamReader(tempsavepath))
                {
                    savedstorage = file.ReadLine().Replace(@"""", "'");
                }
            }
            else
            {
                using (StreamWriter createdfile = new StreamWriter(tempsavepath))
                {
                    createdfile.WriteLine("[]");
                }
            }
            string stringinject;
            stringinject = @"document.getElementsByTagName('html')[0].innerHTML = '<head></head><body></body>';";
            await execScriptHelper(stringinject);
            stringinject = @"
    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'>
    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css'>
    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.css'>
    <style>
        body {
            font-family: sans-serif;
            height: 100%;
            background-image: url('https://appassets/background.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
            overflow-x: hidden;
        }

        .row {
            display: grid;
            grid-template-columns: 21% 21% 21% 21% 21%;
            margin-left: 5%;
            justify-content: center;
        }

            .btn-group-vertical > .btn-group:after, .btn-group-vertical > .btn-group:before, .btn-toolbar:after, .btn-toolbar:before, .clearfix:after, .clearfix:before, .container-fluid:after, .container-fluid:before, .container:after, .container:before, .dl-horizontal dd:after, .dl-horizontal dd:before, .form-horizontal .form-group:after, .form-horizontal .form-group:before, .modal-footer:after, .modal-footer:before, .modal-header:after, .modal-header:before, .nav:after, .nav:before, .navbar-collapse:after, .navbar-collapse:before, .navbar-header:after, .navbar-header:before, .navbar:after, .navbar:before, .pager:after, .pager:before, .panel-body:after, .panel-body:before, .row:after, .row:before {
                content: none;
            }

        .list img {
            object-fit: cover;
            min-height: 100%;
            width: 100%;
            cursor: pointer;
            padding-bottom: 9%;
            padding-right: 9%;
        }

        #myModal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.9);
        }

        .modal-content {
            margin: auto;
            display: block;
            vertical-align: middle;
        }

        .modal-content {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0);
            }

            to {
                -webkit-transform: scale(1);
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0);
            }

            to {
                transform: scale(1);
            }
        }

        .close {
            position: absolute;
            top: 15px;
            right: 35px;
            color: #f1f1f1;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }

        * {
            box-sizing: border-box;
        }

        .slideshow-container {
            justify-content: center;
            display: flex;
        }

        .menushow-container a {
            align-items: center;
            color: #FFFFFF;
        }

        .menushow-container div {
            align-items: center;
            color: #FFFFFF;
        }

        .slideshow-container .mySlides img, .slideshow-container .mySlides iframe {
            padding-left: 10%;
            padding-right: 10%;
            border: none;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
        }

        .goto, .gotochannel, .collaspse, .folderminus, .foldersave, .folderopen, .fileminus, .folderplus, .fileplus, .filechange, .filelink {
            cursor: pointer;
            text-align: center;
            color: white;
            overflow: hidden;
        }

        .centered {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            color: white;
        }

            .centered:hover {
                background-color: black;
            }

        .prev, .next {
            cursor: pointer;
            position: absolute;
            top: 50%;
            width: auto;
            padding: 16px;
            margin-top: -22px;
            color: white;
            font-weight: bold;
            font-size: 18px;
            transition: 0.6s ease;
            border-radius: 0 3px 3px 0;
            user-select: none;
            text-decoration: none;
        }

        .prev {
            left: 0;
            border-radius: 3px 0 0 3px;
            text-decoration: none;
        }

        .next {
            right: 0;
            border-radius: 3px 0 0 3px;
            text-decoration: none;
        }

            .prev:hover, .next:hover {
                background-color: rgba(0,0,0,0.8);
                text-decoration: none;
            }

        .active, .dot:hover {
            background-color: #717171;
        }

        .icon-upload > input {
            display: none;
        }

        .icon-download > input {
            display: none;
        }


        .hide {
            display: none;
        }

        .show {
            display: flex;
        }

        .spinner {
            position: absolute;
            top: calc(50% - 10vh);
            left: calc(50% - 10vh);
            text-align: center;
            font-size: 20vh;
        }

        a {
            color: #FFFFFF;
        }

        ::-webkit-scrollbar {
            width: 10px;
        }

        ::-webkit-scrollbar-track {
            background: #000;
        }

        ::-webkit-scrollbar-thumb {
            background: #888;
        }

            ::-webkit-scrollbar-thumb:hover {
                background: #eee;
            }
    </style>
	<style>
		#dialogoverlay {
			display: none;
			opacity: .8;
			position: fixed;
			top: 0px;
			left: 0px;
			background: #FFF;
			width: 100%;
			z-index: 10;
		}

		#dialogbox {
			display: none;
			position: fixed;
			background: #000;
			border-radius: 7px;
			width: 550px;
			z-index: 10;
		}

			#dialogbox > div {
				background: #FFF;
				margin: 8px;
			}

				#dialogbox > div > #dialogboxhead {
					background: #666;
					font-size: 19px;
					padding: 10px;
					color: #CCC;
				}

				#dialogbox > div > #dialogboxbody {
					background: #333;
					padding: 20px;
					color: #FFF;
				}

				#dialogbox > div > #dialogboxfoot {
					background: #666;
					padding: 10px;
					text-align: right;
				}
				#dialogboxbody input {
					width: 485px;
                    color: black;
				}
	</style>
".Replace("\r\n", " ");
            stringinject = @"""" + stringinject + @"""";
            stringinject = @"document.getElementsByTagName('head')[0].innerHTML = " + stringinject + @";";
            await execScriptHelper(stringinject);
            string stringcontent = @"

	<div id='dialogoverlay'></div>
	<div id='dialogbox'>
		<div>
			<div id='dialogboxhead'></div>
			<div id='dialogboxbody'></div>
			<div id='dialogboxfoot'></div>
		</div>
	</div>

    <div class='bg'>
        <div class='menu'></div>
        <div class='list'></div>
        <div id='myModal' class=''>
            <div class='slideshow-container'></div>
            <div class='menushow-modal-container'></div>
            <span class='close' title='close contents'>&times;</span>
        </div>
        <div class='fa fa-spinner fa-spin spinner hide'></div>
    </div>

    <script>

const bridge = chrome.webview.hostObjects.bridge;

var myfavours = {};
var objdata = {};
var span = document.getElementsByClassName('close')[0];
var modal = document.getElementById('myModal');
var slideIndex = 1;
var tempclass = '';
var resindicex = window.screen.availWidth;
var resindicey = window.screen.availHeight;

function changeTitle() {
    document.title = 'my-favours by michael franiatte';
}

$.ajax({
    url: 'https://www.youtube.com/iframe_api',
    dataType: 'script'
}).done(function() {
    loadPlayer();
});

function loadPlayer() {
    window.onYouTubePlayerAPIReady = function() {
        changeTitle();
        createMyfavours();
    };
}

span.onclick = function(){ 
    modal.style.display = 'none';
    $('.slideshow-container').empty();
    $('body').css('overflow-y', 'auto');
};

function openModal(x) {
    x.onclick = function(){
        createModal(x);
        modal.style.display = 'block';
        slideIndex = 1;
        showSlides(slideIndex);
    };
}

function plusSlides(n) {
  showSlides(slideIndex += n);
}

function currentSlide(n) {
  showSlides(slideIndex = n);
}

function showSlides(n) {
  var i;
  var slides = document.getElementsByClassName('mySlides');
  if (n > slides.length) {
    slideIndex = 1;
  }
  if (n < 1) {
    slideIndex = slides.length;
  }
  for (i = 0; i < slides.length; i++) {
      slides[i].style.display = 'none';
  }
  slides[slideIndex-1].style.display = 'block';
  var link = $('.mySlides:visible').data('link');
  var downloadlink = document.getElementById('download');
  link = link.replace('https://www.youtube.com/embed/', 'https://www.youtube.com/watch?v=');
  downloadlink.href = link.replace('https://www.youtu.be/watch?v=', 'https://www.youtube.com/watch?v=');
}

function createModal(x) {
    $('.menushow-modal-container').html('');
    var htmlString = ``;
    htmlString = `<div class=\'bg-light fileminus\' style=\'display:float;position:absolute;float:right;right:100px;\' onclick=\'contentminus();\' title=\'remove a content\'>
                    <i class=\'fa fa-minus\'></i></div>
                    <div class=\'bg-light fileplus\' style=\'display:float;position:absolute;float:right;right:70px;\' onclick=\'contentplus();\' title=\'add a content\'>
                    <i class=\'fa fa-plus\'></i></div>
                    <div class=\'bg-light filechange\' style=\'display:float;position:absolute;float:right;right:40px;\' onclick=\'changefavour();\' title=\'change favour\'>
                    <i class=\'fa fa-pencil\'></i></div>
                    <a href=\'\' class=\'bg-light\' style=\'display:float;position:absolute;float:right;right:10px;\' id=\'download\' title=\'go to content\'>
                    <i class=\'fa fa-download\'></i></a>`;
    var element = document.getElementById('myModal');
    if (tempclass != '') {
        element.classList.remove(tempclass);
    }
    element.classList.add(x.src);
    tempclass = x.src;
    $('.menushow-modal-container').append(htmlString);
    var files = objdata[x.src] || [];
    $('.slideshow-container').html('');
    htmlString = ``;
    for (let file of files) {
        if (!file.includes('www.youtu')) {
            htmlString += `<div class=\'mySlides\' data-link=\'` + file + `\'>
                                <img src=\'` + file + `\' class=\'content\' style=\'width:80%\'>
                            </div>`;
        }
    }
    for (let file of files) {
        if (file.includes('www.youtu')) {
            file = file.replace('https://www.youtube.com/watch?v=', 'https://www.youtube.com/embed/');
            file = file.replace('https://www.youtu.be/watch?v=', 'https://www.youtube.com/embed/');
            htmlString += `<div class=\'mySlides\' data-link=\'` + file + `\'>
                                <iframe src=\'` + file + `\' frameborder=\'0\' allowfullscreen class=\'content\' style=\'width:` + resindicex * 80 / 100 + `px;height:` + 6.6 / 16 * resindicex * 80 / 100 + `px;\'></iframe>
                            </div>`;
        }
    }
    htmlString += `<a class=\'prev\' onclick=\'plusSlides(-1)\'>&#10094;</a>
                  <a class=\'next\' onclick=\'plusSlides(1)\'>&#10095;</a>`;
    $('.slideshow-container').append(htmlString);
    $('body').css('overflow-y', 'hidden');
}

var folderprompt;

function contentminus() {
    var element = document.getElementById('myModal');
    folderprompt = element.className;
    if (folderprompt != '') {
        Prompt.render('Please enter a content link to delete from ' + folderprompt + ' favour:', 'getcontentminus');
    }
}

function getcontentminus(val) {
    var item = val;
    if (!(item == null || item == '')) {
        myfavours = JSON.parse(JSON.stringify(savedstorage) || '[]');
        myfavours = transformObj(myfavours);
        var newmyfavours = [];
        myfavours.forEach(function(val, index) {
            if ((val.content != item & val.myfavour == folderprompt) | val.myfavour != folderprompt) {
                newmyfavours.push({content: val.content, myfavour: val.myfavour});
            }
        });
        var tempgrouper = newmyfavours;
        var grouped = transformArr(tempgrouper);
        grouped = transformInpand(grouped);
        bridge.SaveStorage(JSON.stringify(grouped));
    }
  	if (item == null | item == '') {
    		return;
  	} 
}

function contentplus() {
    var element = document.getElementById('myModal');
    folderprompt = element.className;
    if (folderprompt != '') {
        Prompt.render('Please enter a content link to add in ' + folderprompt + ' favour:', 'getcontentplus');
    }
}

function getcontentplus(val) {
    var item = val;
    if (!(item == null || item == '')) {
        myfavours = JSON.parse(JSON.stringify(savedstorage)) || [];
        myfavours = transformObj(myfavours);
        myfavours.push({content: item, myfavour: folderprompt});
        var tempgrouper = myfavours;
        var grouped = transformArr(tempgrouper);
        grouped = transformInpand(grouped);
        bridge.SaveStorage(JSON.stringify(grouped));
    }
}

function transformInpand(orig) {
    var grouped = [];
    orig.forEach(function(val, index) {
        var name = val.myfavour;
        var files = val.contents;
        var tempgrouped = [];
        for (let file of files) {
            var content = file.content;
            tempgrouped.push(content);
         };
        grouped.push({contents : tempgrouped, myfavour: name});
    });
    return grouped;
}

function transformObj(orig) {
    myfavours = [];
    orig.forEach(function(val, index) {
        var name = val.myfavour;
        var files = val.contents;
        for (let file of files) {
             myfavours.push({content: file, myfavour: name});
         };
    });
    return myfavours;
}

function transformArr(orig) {
    var newArr = [], myfavours = {}, i, j, cur;
    for (i = 0, j = orig.length; i < j; i++) {
        cur = orig[i];
        if (!(cur.myfavour in myfavours)) {
            myfavours[cur.myfavour] = {myfavour: cur.myfavour, contents: []};
            newArr.push(myfavours[cur.myfavour]);
        }
        myfavours[cur.myfavour].contents.push({content: cur.content});
    }
    return newArr;
}

function createMyfavours() {
    $('.spinner').removeClass('hide');
    $('.spinner').addClass('show');
    myfavours = JSON.parse(JSON.stringify(savedstorage) || '[]');
    myfavours = transformObj(myfavours);
    try {
        var grouped = transformArr(myfavours);
        grouped.forEach(function(val, index) {
            var name = val.myfavour;
            var files = val.contents;
            var array = [];
            var n = 0;
            for (let file of files) {
                if (file.content != '') {
                    var content = file.content;
    	            array.push(content);
                }
             };
             objdata[name] = array;
        });
    }
    catch { }
    var keyNames = Object.keys(objdata);
    let htmlString = '';
    $('.menu').html('');
    htmlString = `<div class=\'bg-light folderminus\' style=\'display:float;position:absolute;left:10px;top:0px;\' onclick=\'listminus();\' title=\'remove a favour\'>
                    <i class=\'fa fa-minus\'></i></div>
                    <div class=\'bg-light folderplus\' style=\'display:float;position:absolute;left:40px;top:0px;\' onclick=\'listplus();\' title=\'add a favour\'>
                    <i class=\'fa fa-plus\'></i></div>
                    <div class=\'icon-download\' style=\'display:float;\'><label for=\'filename\'>
                    <div class=\'bg-light foldersave\' style=\'display:float;position:absolute;left:70px;top:0px;\'>
                    <i class=\'fa fa-save\' title=\'save favours\'></i></div></label>
                    <input type=\'button\' onClick=\'handleFilename()\' value=\'Save\' class=\'button\' id=\'filename\'></div>
                    <div class=\'icon-upload\' style=\'display:float;\'><label for=\'txtFileInput\'>
                    <div class=\'bg-light folderopen\' style=\'display:float;position:absolute;left:100px;top:0px;\'>
                    <i class=\'fa fa-folder-open\' title=\'open favours\'></i></div></label>
                    <input type=\'button\' id=\'txtFileInput\' onClick=\'openStorage()\'></div>`;
    $('.menu').append(htmlString);
    htmlString = '';
    $('.list').html('');
    var countlength = 0;
    var keylength = keyNames.length;
    for (let keyName of keyNames) {
        if (keyName.length > 5) {
            countlength++;
            if (countlength == 1) {
                htmlString = `<div class=\'row\'>`;
            }
            htmlString += `<img onmouseover=\'openModal(this)\' class=\'\' src=\'` + keyName + `\' style=\'width:80%\'>`;
            if (countlength >= keylength) {
                htmlString += `</div>`;
            }
        }
    }
    $('.list').append(htmlString);
    $('.spinner').removeClass('show');
    $('.spinner').addClass('hide');
}

function handleFilename() {
    var txt = JSON.stringify(savedstorage);
    bridge.DownloadTXT(txt);
}

function listminus() {
    Prompt.render('Please enter a favour to delete:', 'getlistminus');
}

function getlistminus(val) {
    var item = val;
    if (!(item == null || item == '')) {
        myfavours = JSON.parse(JSON.stringify(savedstorage) || '[]');
        myfavours = transformObj(myfavours);
        var newmyfavours = [];
        myfavours.forEach(function(val, index) {
            if (val.myfavour != item) {
                newmyfavours.push({content: val.content, myfavour: val.myfavour});
            }
        });
        var tempgrouper = newmyfavours;
        var grouped = transformArr(tempgrouper);
        grouped = transformInpand(grouped);
        bridge.SaveStorage(JSON.stringify(grouped));
    }
}

function listplus() {
    Prompt.render('Please enter a favour to add:', 'getlistplus');
}

function getlistplus(val) {
    var item = val;
    if (!(item == null || item == '')) {
        myfavours = JSON.parse(JSON.stringify(savedstorage)) || [];
        myfavours = transformObj(myfavours);
        myfavours.push({content: '', myfavour: item});
        var tempgrouper = myfavours;
        var grouped = transformArr(tempgrouper);
        grouped = transformInpand(grouped);
        bridge.SaveStorage(JSON.stringify(grouped));
    }
}

function changefavour() {
    var element = document.getElementById('myModal');
    folderprompt = element.className;
    if (folderprompt != '') {
        Prompt.render('Please enter a new favour link to change from ' + folderprompt + ' favour:', 'getchangefavour');
    }
}

function getchangefavour(val) {
    var item = val;
    if (!(item == null || item == '')) {
        myfavours = JSON.parse(JSON.stringify(savedstorage) || '[]');
        myfavours = transformObj(myfavours);
        var newmyfavours = [];
        myfavours.forEach(function(val, index) {
            if (val.myfavour == folderprompt) {
                newmyfavours.push({content: val.content, myfavour: item});
            }
            if (val.myfavour != folderprompt) {
                newmyfavours.push({content: val.content, myfavour: val.myfavour});
            }
        });
        var tempgrouper = newmyfavours;
        var grouped = transformArr(tempgrouper);
        grouped = transformInpand(grouped);
        bridge.SaveStorage(JSON.stringify(grouped));
    }
}

function openStorage() {
    bridge.OpenStorage('');
}

</script>
<script>
    var funcok = '';
	function CustomPrompt() {
	    this.render = function(dialog, func) {
            funcok = func;
	        var winW = window.innerWidth;
	        var winH = window.innerHeight;
	        var dialogoverlay = document.getElementById('dialogoverlay');
	        var dialogbox = document.getElementById('dialogbox');
	        dialogoverlay.style.display = 'block';
	        dialogoverlay.style.height = winH+'px';
	        dialogbox.style.left = (winW/2) - (550 * .5)+'px';
	        dialogbox.style.top = '100px';
	        dialogbox.style.display = 'block';
	        document.getElementById('dialogboxhead').innerHTML = 'A value is required';
	        document.getElementById('dialogboxbody').innerHTML = dialog;
	        document.getElementById('dialogboxbody').innerHTML += `<br><input id=\'prompt_value1\'>`;
	        document.getElementById('dialogboxfoot').innerHTML = `<button onclick=\'Prompt.ok()\'>OK</button> <button onclick=\'Prompt.cancel()\'>Cancel</button>`;
	    };
	    this.cancel = function() {
	        document.getElementById('dialogbox').style.display = 'none';
	        document.getElementById('dialogoverlay').style.display = 'none';
	    };
	    this.ok = function() {
	        var prompt_value1 = document.getElementById('prompt_value1').value;
	        window[funcok](prompt_value1);
	        document.getElementById('dialogbox').style.display = 'none';
	        document.getElementById('dialogoverlay').style.display = 'none';
	    };
	}
	var Prompt = new CustomPrompt();
</script>
<script>

$('body').on('click', 'img', function() {
    var source = this.src;
    var input = document.createElement('textarea');
    input.value = source;
    document.body.appendChild(input);
    input.select();
    document.execCommand('Copy');
    input.remove();
});

function responseFunc() { }

</script>
".Replace("\r\n", " ").Replace("savedstorage", savedstorage);
            stringcontent = @"""" + stringcontent + @"""";
            stringinject = @"(function () {
    // more or less stolen form jquery core and adapted by paul irish
    function getScript(url, success) {
        var script = document.createElement('script');
        script.src = url;
        var head = document.getElementsByTagName('head')[0],
            done = false;
        // Attach handlers for all browsers
        script.onload = script.onreadystatechange = function () {
            if (!done && (!this.readyState
                || this.readyState == 'loaded'
                || this.readyState == 'complete')) {
                done = true;
                success();
                script.onload = script.onreadystatechange = null;
                head.removeChild(script);
            }
        };
        head.appendChild(script);
    }
    getScript('https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js', function () {
        if (typeof jQuery == 'undefined') {
            console.log('Sorry, but jQuery wasn\'t able to load');
        } else {
            console.log('This page is now jQuerified with v' + $.fn.jquery);
            $(document).ready(function () { });
                var script = document.createElement('script'); script.src = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js'; document.head.appendChild(script);
                $(document).ready(function(){$('body').html(stringcontent);
            });
        }
    });
})();".Replace("stringcontent", stringcontent);
            await execScriptHelper(stringinject);
        }
        private void WebView21_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.StartsWith("https://www.youtube.com/watch?v=") | !e.Uri.Contains("youtube.com"))
            {
                Process.Start(e.Uri);
                e.Cancel = true;
            }
            else if (!(e.Uri.StartsWith("https://www.youtube.com/feed/subscriptions") | e.Uri.StartsWith("https://www.youtube.com/embed/")))
                e.Cancel = true;
        }
        private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            string stringinject = @"
                    document.cookie = 'VISITOR_INFO1_LIVE = oKckVSqvaGw; path =/; domain =.youtube.com';
                    var cookies = document.cookie.split('; ');
                    for (var i = 0; i < cookies.length; i++)
                    {
                        var cookie = cookies[i];
                        var eqPos = cookie.indexOf('=');
                        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                        document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:00 GMT';
                    }
                    var el = document.getElementsByClassName('ytp-ad-skip-button');
                    for (var i=0;i<el.length; i++) {
                        el[i].click();
                    }
                    var element = document.getElementsByClassName('ytp-ad-overlay-close-button');
                    for (var i=0;i<element.length; i++) {
                        element[i].click();
                    }
                    var scripts = document.getElementsByTagName('script');
                    for (let i = 0; i < scripts.length; i++)
                    {
                        var content = scripts[i].innerHTML;
                        if (content.indexOf('ytp-ad') > -1) {
                            scripts[i].innerHTML = '';
                        }
                        var src = scripts[i].getAttribute('src');
                        if (src.indexOf('ytp-ad') > -1) {
                            scripts[i].setAttribute('src', '');
                        }
                    }
                    var iframes = document.getElementsByTagName('iframe');
                    for (let i = 0; i < iframes.length; i++)
                    {
                        var content = iframes[i].innerHTML;
                        if (content.indexOf('ytp-ad') > -1) {
                            iframes[i].innerHTML = '';
                        }
                        var src = iframes[i].getAttribute('src');
                        if (src.indexOf('ytp-ad') > -1) {
                            iframes[i].setAttribute('src', '');
                        }
                    }
                    var allelements = document.querySelectorAll('*');
                    for (var i = 0; i < allelements.length; i++) {
	                    var classname = allelements[i].className;
                        if (classname.indexOf('ytp-ad') > -1)  {
                                allelements[i].innerHTML = '';
			            }
                    }
                ".Replace("\r\n", " ");
            await this.webView21.CoreWebView2.ExecuteScriptAsync(stringinject);
        }
        private async System.Threading.Tasks.Task<String> execScriptHelper(String script)
        {
            var x = await webView21.ExecuteScriptAsync(script).ConfigureAwait(false);
            return x;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.webView21.Dispose();
        }
    }
    public static class Extensions
    {
        public static async Task<string> ExecuteScriptFunctionAsync(this WebView2 webView2, string functionName, params object[] parameters)
        {
            string script = functionName + "(";
            for (int i = 0; i < parameters.Length; i++)
            {
                script += JsonConvert.SerializeObject(parameters[i]);
                if (i < parameters.Length - 1)
                {
                    script += ", ";
                }
            }
            script += ");";
            return await webView2.ExecuteScriptAsync(script);
        }
    }
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class Bridge
    {
        public static Form1 form1 = new Form1();
        public static string txt;
        public string SaveStorage(string param)
        {
            string tempsavepath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(@"file:\", "").Replace(Process.GetCurrentProcess().ProcessName + ".exe", "").Replace(@"\", "/").Replace(@"//", "") + "tempsave";
            using (StreamWriter createdfile = new StreamWriter(tempsavepath))
            {
                string str = param;
                createdfile.WriteLine(str);
            }
            form1.OpenUrl("https://www.youtube.com/feed/subscriptions");
            return param;
        }
        public string OpenStorage(string param)
        {
            Thread newThread = new Thread(new ThreadStart(showOpenFileDialog));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
            return param;
        }
        public void showOpenFileDialog()
        {
            string str = "";
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader file = new StreamReader(op.FileName))
                {
                    str = file.ReadLine();
                }
                string tempsavepath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(@"file:\", "").Replace(Process.GetCurrentProcess().ProcessName + ".exe", "").Replace(@"\", "/").Replace(@"//", "") + "tempsave";
                using (StreamWriter createdfile = new StreamWriter(tempsavepath))
                {
                    createdfile.WriteLine(str);
                }
                form1.OpenUrl("https://www.youtube.com/feed/subscriptions");
            }
        }
        public string DownloadTXT(string param)
        {
            txt = param;
            Thread newThread = new Thread(new ThreadStart(showSaveFileAsDialog));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
            return param;
        }
        public void showSaveFileAsDialog()
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "All Files(*.*)|*.*";
            if (sa.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter createdfile = new StreamWriter(sa.FileName))
                {
                    createdfile.WriteLine(txt);
                }
            }
        }
    }
}