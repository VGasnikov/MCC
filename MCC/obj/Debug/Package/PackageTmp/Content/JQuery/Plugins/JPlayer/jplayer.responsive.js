function setImgMargin(){
	var imgMargin = (($('.jp-video').width())-($('.jp-video img').width()))/2;
	if(imgMargin < 0) imgMargin = 0;
	$('.jp-video img').css('margin-left', imgMargin);

}	
	
function setImgHeight(containerId, styleHeight, styleWidth){
	var computedWidth = parseInt($('.jp-video').width(), 10);
	if (computedWidth <= 480){                                            
		var newImgHeight = (styleHeight/styleWidth)*computedWidth;
	}
	$('#'+containerId).css('height', newImgHeight);
	$('#'+containerId+' img').css('height', newImgHeight);
}

function jPlayerResponsiveCss(containerId){
	var parentWidth = $('#'+containerId).width();
	
	switch(true){
		case (parentWidth < 190):                            
			$('#'+containerId+'_container').html('Jplayer does not support a resolution this small');
			break;
		case (parentWidth < 238):
			$('.jp-gui .jp-progress-slider').css('margin-left', '0');
			$('.jp-gui .ui-corner-all:not(.ui-slider):not(.ui-slider-range)').css('margin', '0');
		case (parentWidth < 295):
			$('.jp-gui .ui-corner-all:not(.ui-slider):not(.ui-slider-range)').css('padding', '0');
			$('.jp-gui div.jp-volume-slider').css('margin-top', '2px');
			$('.jplayer .jp-gui.ui-corner-all div.jp-mute').css('margin-left', '5px');
			$('.jplayer .jp-gui.ui-corner-all div.jp-unmute').css('margin-left', '5px');
			$('.jp-gui.ui-widget').css('padding', '1px 3px');
		case (parentWidth < 334):
			$('.jp-gui .jp-duration').hide();
		case (parentWidth < 414):
			$('.jp-gui .jp-current-time').hide();
			$('.jp-gui .jp-progress-slider').css('margin-left', '10px');
		case (parentWidth < 454):
			$('.jp-progress-slider').css('width', '20%');
			$('.jp-volume-slider').css('width', '7.5%');
			break;
		case (parentWidth < 549):
			$('.jp-progress-slider').css('width', '25%');
			$('.jp-volume-slider').css('width', '10%');
			break;
		case (parentWidth < 718):
			$('.jp-progress-slider').css('width', '32.5%');
			$('.jp-volume-slider').css('width', '12.5%');
			break;
		default:
			break;
	}
}