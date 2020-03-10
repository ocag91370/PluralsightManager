var count = 0;
var shouldBeSequential = true;

Number.prototype.myPadding = function () {
  var number = this.valueOf();
  var length = 2;
  var str = '' + number;
  while (str.length < length) {
    str = '0' + str;
  }
  return str;
};

function getVideoSrc(){
  var link = $('#vjs_video_3_html5_api');
  if(!link.length){
    //try fix get src error(domId changed)
    link = $('video');
  }
  return link.attr('src');
}

function getSrtSrc(){
  var link = $('track');
  return 'https://app.pluralsight.com'+ link.attr('src');
}

function pauseVideo() {
  if ($('#play-control').length === 1) {
    $('#play-control').click();
  }
}

function pad(d, len) {
  return String(d).padStart(len, '0');
}

function getElementName(selector) {
  var name = $(selector).text();
  name = name.replace(/[\/:?><]/g, '');
  return name.trim();
}

function getCourse() {
  return getElementName('[class*="course-title__title"] [class*="course-title__link"]');
}
function getModule() {
  var count = $('[class="table-of-contents"]>[data-css-knxp45]').length;
  var padLen = Math.max(count.toString().length, 2);
  var index = pad(parseInt($('[class="table-of-contents"] [class*="is-current-module"]').index()) + 1, padLen);
  var name = getElementName('[class="table-of-contents"] [class*="is-current-module"] [class*="module-header__title"]');
  var result = index + ' - ' + name;
  // alert(result);
  return result;
}
function getItem() {
  var count = $('[class="table-of-contents"] [class*="is-current-module"] [class*="module-content"]>[class*="content-item"]').length;
  var padLen = Math.max(count.toString().length, 2);
  var index = pad(parseInt($('[class="table-of-contents"] [class*="is-current-module"] [class="module-content"] [class*="is-current"]').index()) + 1, padLen);
  var name = getElementName('[class="table-of-contents"] [class*="is-current-module"] [class="module-content"] [class*="is-current"] span[class*="u-truncate"]');
  var result = index + ' - ' + name;
  // alert(result);
  return result;
}

function getCourseName() {
  //var moduleText = $('[class="table-of-contents"] [class*="is-current-module"] [class*="module-header__title"]').text();
  var courseName = $('[class="table-of-contents"] [class*="is-current-module"] [class*="is-current"] [class="u-truncate"]').text();
  // var courseName = $('#course-title-link').text();
  courseName = courseName.replace(/[\/:?><]/g, '');
  return courseName.trim();
}

function getSectionDom() {
  var folderDom = $('li.selected')
    .parents('ul')
    .prev('header')
    .children('div')
    .eq(1);
  return folderDom;
}

function getSaveSrtFilePath(language) {
  // var srtPath = $('[class="player-wrapper"] [id="video-element"] track').Attr("src");
  var itemName = getItem();
  var saveFileName = itemName + '.' + language + '.srt';
  console.log('srt: ' + saveFileName);
  return saveFileName.replace(/(\r\n|\n|\r)/gm, "");
}

function getSaveFilePath() {
  var moduleName = getModule();
  var courseName = getCourse();
  var itemName = getItem();
  console.log('module: ' + moduleName + ' , course: ' + courseName + ' , item: ' + itemName);
  var saveFileName = itemName + '.mp4';
  return saveFileName.replace(/(\r\n|\n|\r)/gm, "");
/*
  var link = getVideoSrc();
  console.log('link: ' + link);

  var courseName = getCourseName();
  console.log(courseName);

  var folderDom = getSectionDom();
  var sectionName = folderDom.find('h2').text();
  var sectionIndex = (folderDom.parents('section.module.open').eq(0).index() + 1).myPadding();
  var saveFolder = sectionIndex + ' - ' + sectionName;
  saveFolder = saveFolder.replace(/[\/:?><]/g, '');
  console.log(saveFolder);

  var rawFileName = $('#module-clip-title').text().split(' : ').pop().trim();
  var fileIndex = ($('li.selected').eq(1).index() + 1).myPadding();
  var saveFileName = fileIndex + ' - ' + rawFileName + '.' + link.split('?')[0].split('.').pop();
  saveFileName = saveFileName.replace(/[\/:?><]/g, '');
  console.log(saveFileName);

  console.log('processing => ' + courseName + ' ' + sectionIndex + ' - ' + fileIndex);
  var saveFilePath = 'Pluralsight/' + courseName + '/' + saveFolder + '/' + saveFileName;
  return saveFilePath.replace(/(\r\n|\n|\r)/gm, "");
*/
}
function download(filename, text) {
  var element = document.createElement('a');
  element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
  element.setAttribute('download', filename);

  element.style.display = 'none';
  document.body.appendChild(element);

  element.click();

  document.body.removeChild(element);
}

function downloadCurrentVideo() {
  var link = getVideoSrc();
  console.log('downloadCurrentVideo: ' + link);
  var saveFilePath = getSaveFilePath();
  console.log('chrome download => ' + saveFilePath);
  chrome.runtime.sendMessage({
      action: 'download',
      link: link,
      filename: saveFilePath
    },
    function (response) {
      console.log('=> ' + response.actionStatus);
    }
  );
}


function downloadCurrentSrt(language) {
  var link = getSrtSrc();
  link = link.substr(0, link.length - 3) + language;
  console.log('downloadCurrentSrt: ' + link);
  var saveFilePath = getSaveSrtFilePath(language);
  console.log('chrome download => ' + saveFilePath);
  chrome.runtime.sendMessage({
      action: 'download',
      link: link,
      filename: saveFilePath
    },
    function (response) {
      console.log('=> ' + response.actionStatus);
      // download(saveSrtFilePath, response);
    }
  );
}

function downloadAllVideos() {
  var link = getVideoSrc();
  var saveFilePath = getSaveFilePath();
  console.log('chrome download => ' + saveFilePath);

  var downloadAllVideosTimeout = 30000;
  var pauseVideoTimeout = 8000;
  var folderDom = getSectionDom();
  var sectionName = folderDom.find('h2').text();
  var finalFolderName = $('section:last').find('h2').text();
  var rawFileName = $('#module-clip-title').text().split(' : ').pop().trim();
  var finalFileName = $('section:last').find('li:last').find('h3').text();

  chrome.runtime.sendMessage({
      action: shouldBeSequential? 'download-sync': 'download',
      link: link,
      filename: saveFilePath
    },
    function (response) {
      console.log('response => ' + response.actionStatus);
      if (sectionName == finalFolderName && rawFileName == finalFileName) {
        alert("Full Course Downloaded!");
      } else {
        $('#next-control').click();
        // Use less timeout in sequential mode, since the
        // response is  already async.
        setTimeout(pauseVideo, shouldBeSequential? pauseVideoTimeout / 2 : pauseVideoTimeout);
        setTimeout(downloadAllVideos, shouldBeSequential? downloadAllVideosTimeout / 3 :  downloadAllVideosTimeout);
      }
    }
  );
}

$(document).on('keydown', function ( e ) {
  // You may replace `c` with whatever key you want
  if ((e.metaKey || e.ctrlKey) && (e.metaKey || e.shiftKey) && ( String.fromCharCode(e.which).toLowerCase() === 'v') ) {
    console.log('s => current');
    downloadCurrentVideo();
  }
  if ((e.metaKey || e.ctrlKey) && (e.metaKey || e.shiftKey) && ( String.fromCharCode(e.which).toLowerCase() === 'e') ) {
    console.log('s => current');
    downloadCurrentSrt('en');
  }
  if ((e.metaKey || e.ctrlKey) && (e.metaKey || e.shiftKey) && ( String.fromCharCode(e.which).toLowerCase() === 'f') ) {
    console.log('s => current');
    downloadCurrentSrt('fr');
  }
});

// $(function () {
//   $(document).keypress(function (e) {
//     if (e.ctrlKey)
//     {
//       if (e.which === 115 || e.which === 83) {
//         alert('downloadCurrentVideo');
//         // keypress `s`
//         console.log('s => current');
//         //downloadCurrentVideo();
//       } else if (e.which === 97 || e.which === 65) {
//         // keypress `a`
//         count = 0;
//         // shouldBeSequential = confirm('Do you want your downloads to be sequential?');

//         console.log('a => all');
//         //downloadAllVideos();
//       }
//     }
//   });
// });
