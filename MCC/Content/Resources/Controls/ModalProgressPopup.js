function ModalProgressPopupLayout(divPopup, width, height) {
	var clientWidth;
	if (window.innerWidth) {
		clientWidth = ((Sys.Browser.agent === Sys.Browser.Safari) ? window.innerWidth : Math.min(window.innerWidth, document.documentElement.clientWidth));
	} else {
		clientWidth = document.documentElement.clientWidth;
	}
	var clientHeight;
	if (window.innerHeight) {
		clientHeight = ((Sys.Browser.agent === Sys.Browser.Safari) ? window.innerHeight : Math.min(window.innerHeight, document.documentElement.clientHeight));
	} else {
		clientHeight = document.documentElement.clientHeight;
	}
	divPopup.style.left = ((clientWidth - width) / 2) + 'px';
	divPopup.style.top = ((clientHeight - height) / 2) + 'px';
}

function ModalProgressScrollToTop() {
    	window.scroll(0, 0);
    	setTimeout('ModalProgressScrollToTop()',100); // scroll to the top repeatedly in case the user scrolls to the bottom again.
}