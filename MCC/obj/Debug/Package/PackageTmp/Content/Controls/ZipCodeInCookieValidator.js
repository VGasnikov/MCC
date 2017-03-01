function setZipCodeInCookie(name, value, expires) {
    //alert("expires :" + expires);
    //alert("value :" + value);
    var exdate = new Date();
    exdate.setTime(exdate.getTime() + (expires * 24 * 60 * 60 * 1000));
    //exdate.setDate(exdate.getDate() + expires);
    var cookieValue = escape(value) + ((expires == null) ? "" : ";expires=" + exdate.toGMTString()) + ";path=/";
    document.cookie = name + "=" + cookieValue;
}
function getZipCodeCookie(name) {

    var allCookies = document.cookie.split(';');
    var i, tempCookie, cookieName, cookieValue, cookieFound = false;
    for (i = 0; i < allCookies.length; i++) {
        tempCookie = allCookies[i].split('=');
        cookieName = tempCookie[0].replace(/^\s+|\s+$/g, '');
        if (cookieName == name) {
            cookieFound = true;
            if (tempCookie.length > 1) {
                cookieValue = unescape(tempCookie[1].replace(/^\s+|\s+$/g, ''));
                //alert("cookieValue :" + cookieValue);
            }
            return cookieValue;
            break;
        }
        tempCookie = null;
        cookieName = '';
    }
    if (!cookieFound) {
        return null;
    }

}
//		function checkCookie() {
//			var zipCode = getZipCodeCookie("zipCodeCookie");
//			alert("zip code " + zipCode);
//			
//			//var valid = zipCodePattern.test(zipCode);
//			if (zipCode != null && zipCode != "") {
//				document.frmZipCode.txtZipCode.value = zipCode;
//				
//			}
//			else {
//				updateCookie();
//			}

//		}

function updateCookie() {
    var code = document.frmZipCode.txtZipCode.value;
    var zipCodePattern = /^\d{5}$|^\d{5}-\d{4}$/;
    if (code != null && code != "") {
        if (zipCodePattern.test(code)) {
            deleteCookie("zipCodeCookie1", "", -1)
            setZipCodeInCookie("zipCodeCookie1", code, 1);
            alert("ZipCode has been set in cookie.");
        } else {
            alert("Please enter a valid zipcode.");
            document.frmZipCode.txtZipCode.focus();
        }
    } else {
        alert("Please enter zipcode.");
    }
}


function deleteCookie(name, value, days) {
    var date = new Date();
    //alert("deleteCookie name " + name);
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    var expires = "; expires=" + date.toGMTString();
    //alert("expires " + expires);
    document.cookie = name + "=" + value + expires;
}

function populateCookie() {
    var zipCode = getZipCodeCookie("zipCodeCookie1");
    //alert("zip code " + zipCode);

    //var valid = zipCodePattern.test(zipCode);
    if (zipCode != null && zipCode != "") {
        document.frmZipCode.txtZipCode.value = zipCode;

    }
}