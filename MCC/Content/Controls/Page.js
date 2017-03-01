/// <reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.0-vsdoc.js" />

function ClipboardCopy(text, removeFormat) {
    alert('The copy to clipboard feature has been removed due to limited browser support.');
}

function openHelpWindow(url) {
	//openWindow(url, 'Help', 600, 400, -1, -1, false, true, false);
	openWindow(url, 'Help', 800, 600, -1, -1, false, true, false);
}

function openPrintWindow(url) {
    openWindow(url, 'Print', 600, 500, -1, -1, false, true, false);
}

function openPreviewWindow(url) {
    openWindow(url, 'Preview', 820, 600, -1, -1, false, true, true);
}

function openWindow(url, name, width, height, left, top, popUnder, scrollBars, resizable, menuBar, status, toolBar, location, directories) {
    if (name == null) name = 'OpenWindow';
    if (width == null) width = 600;
    if (height == null) height = 400;
    if (left == null) left = -1;
    if (top == null) top = -1;
    if (popUnder == null) popUnder = false;
    if (scrollBars == null) scrollBars = true;
    if (resizable == null) resizable = true;
    if (menuBar == null) menuBar = false;
    if (status == null) status = false;
    if (toolBar == null) toolBar = false;
    if (location == null) location = false;
    if (directories == null) directories = false;

    var xpos = (left >= 0) ? left : ((screen.width - width) / 2);
    var ypos = (top >= 0) ? top : ((screen.height - height) / 2);

    var newWindow = window.open(url, name, 'menubar=' + yesNo(menuBar) + ',status=' + yesNo(status) + ',scrollbars=' + yesNo(scrollBars)
		+ ',resizable=' + yesNo(resizable) + ',height=' + height + ',width=' + width + ',left=' + xpos + ',top=' + ypos + ',screenX=' + xpos + ',screenY=' + ypos
		+ ',toolbar=' + yesNo(toolBar) + ',location=' + yesNo(location) + ',directories=' + yesNo(directories) + '');

    if (popUnder) newWindow.blur();
}

function yesNo(value) {
    return value ? 'yes' : 'no';
}

function resizeText(control, percent) {
    if (control.style.fontSize == '') control.style.fontSize = '100%';
    control.style.fontSize = parseFloat(control.style.fontSize) + percent + '%';
}

function toggleDiv(divId, remember) {
    var style = $get(divId).style;
    if (style.display == 'block' || style.display == '') {
        style.display = 'none';
        if (remember) {
            var now = new Date();
            fixCookieDate(now);
            now.setTime(now.getTime() + 365 * 24 * 60 * 60 * 1000);
            setCookie('CPTOGGLEDIV_' + divId, 'HIDE', now);
        }
    } else {
        style.display = 'block';
        if (remember) removeCookie('CPTOGGLEDIV_' + divId);
    }
}

function findOffset(obj) {
    var x = y = 0;
    if (obj.offsetParent) {
        do {
            x += obj.offsetLeft;
            y += obj.offsetTop;
        } while (obj = obj.offsetParent);
    }
    return [x, y];
}

function setCookie(name, value, expires, path, domain, secure) {
    var curCookie = name + '=' + escape(value)
		+ ((expires) ? '; expires=' + expires.toGMTString() : '')
		+ ((path) ? '; path=' + path : '; path=/')
		+ ((domain) ? '; domain=' + domain : '')
		+ ((secure) ? '; secure' : '');
    document.cookie = curCookie;
}

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + '=';
    var begin = dc.indexOf('; ' + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else
        begin += 2;
    var end = document.cookie.indexOf(';', begin);
    if (end == -1) end = dc.length;
    return unescape(dc.substring(begin + prefix.length, end));
}

function removeCookie(name, path, domain) {
    if (getCookie(name)) {
        document.cookie = name + '='
			+ ((path) ? '; path=' + path : '; path=/')
			+ ((domain) ? '; domain=' + domain : '')
			+ '; expires=Thu, 01-Jan-70 00:00:01 GMT';
    }
}

function fixCookieDate(date) {
    var base = new Date(0);
    var skew = base.getTime();
    if (skew > 0) date.setTime(date.getTime() - skew);
}