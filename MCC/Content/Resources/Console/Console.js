/// <reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.0-vsdoc.js" />

function RowSelectorColumnRegister(parentName, childName) {
    if (typeof (document.getElementById) == 'undefined') return;

    var parent = document.getElementById(parentName);
    var child = document.getElementById(childName);
    if ((parent == null) || (child == null)) return;

    if (typeof (parent.participants) == 'undefined')
        parent.participants = new Array();
    parent.participants[parent.participants.length] = child;
}

function RowSelectorColumnSelectAll(parentCheckBox, itemClass, alternatingItemClass, selectedItemClass) {
    if (typeof (document.getElementById) == 'undefined') return;
    if ((parentCheckBox == null) || (typeof (parentCheckBox.participants) == 'undefined')) return;

    var participants = parentCheckBox.participants;
    for (var i = 0; i < participants.length; i++) {
        var participant = participants[i];
        if (participant != null) {
            if (!participant.disabled) {
                participant.checked = parentCheckBox.checked;
                var rowClass = itemClass;
                if (((i + 1) % 2) == 0) rowClass = alternatingItemClass;
                RowSelectorColumnSelect(participant, rowClass, selectedItemClass);
            }
        }
    }
}

function RowSelectorColumnSelect(checkBox, rowClass, selectedItemClass) {
    if (typeof (document.getElementById) == 'undefined') return;
    if (checkBox == null) return;

    var row = checkBox;
    while ((row.tagName.toUpperCase() != 'TR') && (row != null))
        row = document.all ? row.parentElement : row.parentNode;

    if (checkBox.checked) {
        row.className = selectedItemClass;
    } else {
        row.className = rowClass;
    }
}

function SessionTimer(min, sec) {
    sec--;
    if (sec == -1) { sec = 59; min--; }
    window.status = 'Your session will time out in approximately ' + min + ' min ' + sec + ' sec. ';
    if (min != 0 || sec != 0) {
        setTimeout('SessionTimer(' + min + ', ' + sec + ')', 1000);
    }
    else {
        window.status = 'Your session has timed out.';
        alert('Your session has timed out. Please save your data before refreshing.');
    }
}

function toggleNav(navId, mainId, bodybg, remember) {
    var navStyle; var mainStyle;

    if (document.getElementById)	// this is the way the standards work
    {
        navStyle = document.getElementById(navId).style;
        mainStyle = document.getElementById(mainId).style;
    }
    else if (document.all)			// this is the way old msie versions work
    {
        navStyle = document.all[navId].style;
        mainStyle = document.all[mainId].style;
    }
    else if (document.layers)		// this is the way nn4 works
    {
        navStyle = document.layers[navId].style;
        mainStyle = document.layers[mainId].style;
    }

    if (navStyle.display == 'block' || navStyle.display == '')	// hide navigation
    {
        navStyle.display = 'none';
        mainStyle.marginLeft = '0';
        document.body.style.backgroundImage = 'none';
        document.documentElement.style.backgroundImage = 'none';
        if (remember) {
            var now = new Date();
            fixCookieDate(now);
            now.setTime(now.getTime() + 365 * 24 * 60 * 60 * 1000);
            setCookie('CPTOGGLENAV_' + navId + '_' + mainId, 'HIDE', now);
        }
    }
    else														// display navigation
    {
        navStyle.display = 'block';
        mainStyle.marginLeft = navStyle.width;
        document.body.style.backgroundImage = 'url(' + bodybg + ')';
        document.body.style.backgroundPosition = 'top left';
        document.body.style.backgroundRepeat = 'repeat-y';
        document.documentElement.style.backgroundImage = 'url(' + bodybg + ')';
        document.documentElement.style.backgroundPosition = 'top left';
        document.documentElement.style.backgroundRepeat = 'repeat-y';
        if (remember) removeCookie('CPTOGGLENAV_' + navId + '_' + mainId);
    }
}

var toolboxDroppable;
function toggleToolbox() {
	// this feature was developed for client consoles only, the reference is added and this is enforced in Centralpoint.Web.UI: Console.cs: FormRowGroup
	if (typeof (JSON) === "undefined") {
		alert("The console toolbox will not work in your browser.  Please upgrade to a newer browser to use this feature.");
		return;
	}

	var $toolbox = $("#divToolbox");
	var isHidden = $toolbox.is(":visible");	// if the toolbox is visible before it is toggled it is being hidden by this method

	var top = $("#divHeader").height();
	var offset = $(window).scrollTop();
	if (offset > 0) {
		top = (offset < top) ? (top - offset) : 0;
		//console.log("offset", offset, top);
	}

	var interval = 500; var width = $("#cnNavigation").width();
	$('#divMain').animate({ "margin-right": isHidden ? "-=" + width : "+=" + width }, interval);
	$toolbox.css("width", width).css("top", top).toggle("slide", { direction: "right" }, interval, function () {
		$(".ConsoleFormInput .RadEditor").each(function (index) {
			var editor = $find($(this).attr("id"));
			editor.repaint();
		});
	});
	if (isHidden) return;	// do not initialize the accordion if when the toolbox is hidden

	var $toolboxAccordion = $("#divToolboxAccordion");
	if ($toolboxAccordion.hasClass("ui-accordion")) return;	// do not initialize the accordion when it has already been initialized

	var module = "Template";
	var views = "";
	if (typeof (cpsys) !== "undefined") {
		if (typeof (cpsys.SiteMapInfo) !== "undefined")
			module = cpsys.SiteMapInfo.SystemName;
		if (typeof (cpsys.ModuleViewsInfo) !== "undefined")
			views = cpsys.ModuleViewsInfo.NavigationItemId + ':' + cpsys.ModuleViewsInfo.GroupId;
	}
	var sbUrl = "/Console/ScriptBuilder/Dialog.aspx?edit=1&module=" + module + "&views=" + views;
	var sbWidth = 800, sbHeight = 600;
	if ((window.innerWidth > 1024) && (window.innerHeight > 768)) { sbWidth = 1024; sbHeight = 768; }
	var sbDialog = $find("rwScriptBuilder");
	sbDialog.setSize(sbWidth, sbHeight);
	var pasteText = function ($destination, html) {
		var oldValue = $destination.val();
		if (oldValue.length <= 0) {
			$destination.val(html);
			return;
		}

		var input = $destination.get(0);
		if (!("selectionStart" in input)) { // IE8
			$destination.val($destination.val() + html);
			return;
		}

		// IE9 does not properly replace selections in the middle of a text area, but it does insert the text at the beggining or end properly.
		var prefix = (input.selectionStart > 0) ? oldValue.substring(0, input.selectionStart) : "";
		var suffix = (input.selectionEnd < oldValue.length) ? oldValue.substring(input.selectionEnd, oldValue.length) : "";
		var selectionStart = input.selectionStart;
		$destination.val(prefix + html + suffix);
		input.selectionStart = selectionStart;
		input.selectionEnd = (selectionStart + html.length);
		input.focus();
	}
	sbDialog.add_close(function (sender, args) {
		//console.log("close", sender.argument);
		var arg = args.get_argument();
		if (arg) pasteText($(sender.argument.destination), arg.html);
	});

	$.ajax({
		type: "POST",
		url: "/Console/WebServices/ClientMethods.asmx/ToolboxAccordionHtml",
		data: JSON.stringify({ module: module, views: views }),
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: function (data) {
			//console.log(data.d);
			$toolboxAccordion.html(data.d);

			$toolboxAccordion.find(".toolbox-source img").each(function (index) {
				var $img = $(this);
				$img.attr("data-imgsrc", $img.attr("src"));
				$img.removeAttr("src");
			});

			$toolboxAccordion.tooltip({
				show: { effect: "slideDown", delay: 750 },
				position: { my: "right top", at: "left-5 top" }
			});

			var index = 0;
			var $defaultGroup = $toolboxAccordion.find("h3[data-isdefault='true']:first");
			if ($defaultGroup.length >= 1) {
				index = $defaultGroup.data("index");
			} else {
				var $standardScripts = $toolboxAccordion.find("h3:contains('Standard Scripts'):first");
				if ($standardScripts.length >= 1) index = $standardScripts.data("index");
			}

			$("#divToolboxLoading").hide();
			var helpUrl = $("#trailHelpLink").parent().data("helpurl");
			if (!!helpUrl) {
				$toolbox.prepend("<div id=\"divToolboxModuleHelp\"><a href=\"javascript:openHelpWindow(\'" + helpUrl + "\')\">Module Help File</a></div>")
			}
			$toolboxAccordion.accordion({ active: index, heightStyle: "fill" });

			$(".toolbox-draggable").draggable({
				revert: false,
				appendTo: "body",
				zIndex: 2,
				cursor: "pointer",
				cursorAt: { top: 6, left: 3 },
				iframeFix: true,
				helper: function (e, ui) {
					return $("<div></div>").append($(this).find(".toolbox-preview").clone().show());
				},
				start: function (event, ui) {
					$(this).fadeTo('fast', 0.5);
					//if (!$.browser.chrome) ui.position.top -= $(window).scrollTop();
				},
				drag: function(event, ui) {
					if (!$.browser.chrome) ui.position.top -= $(window).scrollTop();	// http://stackoverflow.com/questions/5791886/jquery-draggable-shows-helper-in-wrong-place-when-scrolled-down-page: I'm hoping that this problem is resolved in future versions of jQuery.
				},
				stop: function (event, ui) {
					$(this).fadeTo(0, 1);
				}
			});

			$(window).on("resize scroll", function (e) {
				var $toolbox = $("#divToolbox");
				if ($toolbox.is(":hidden")) return;

				var offset = $(window).scrollTop();
				var headerHeight = $("#divHeader").height();
				if (offset < headerHeight) {
					$toolbox.css({ top: (headerHeight - offset), bottom: 0 });
					$("#divToolboxAccordion").accordion("refresh");
				} else if ($toolbox.top != 0) {
					$toolbox.css({ top: 0, bottom: 0 });
					$("#divToolboxAccordion").accordion("refresh");
				}
			});
		},
		error: function (xhr, status, error) {  /* alert(xhr.responseText); */  }
	});

	toolboxDroppable = {
		hoverClass: "toolbox-active",
		tolerance: "pointer",
		over: function (event, ui) {
			if (($(ui.draggable).data("type") == "scriptbuilder") && !$(this).closest(".ConsoleFormInput").data("allowscripts"))
				$("body").css("cursor", "no-drop");
		},
		out: function (event, ui) {
			$("body").css("cursor", "pointer");
		},
		drop: function (event, ui) {
			var $destination = $(this);
			var $tool = $(ui.draggable);
			var source = $tool.find(".toolbox-source").html().replace(/ data-imgsrc=/ig, " src=");

			if ($destination.is(".RadEditor")) {
				var editor = $find($(this).attr("id"));
				var isHtmlMode = (editor.get_mode() == 2);
				if (!isHtmlMode) {
					editor.setFocus();
					if (window.navigator.userAgent.toUpperCase().indexOf(" EDGE/") > 0) editor.get_contentArea().focus(); // http://www.telerik.com/account/support-tickets/view-ticket.aspx?threadid=988298: This " EDGE/" case was added because the editor.setFocus() method contains a bug that prevents it from working in Edge: http://feedback.telerik.com/Project/108/Feedback/Details/172383-the-client-side-setfocus-method-does-not-work-in-edge-browser.
				}

				var pasteHtml = function (sender, args) {
					var html = args.html;
					if (args.type !== "scriptbuilder") {
						html = html.replace(/('|")\/Resource.ashx/ig, "$1/Console/Resource.ashx");
					}

					var sel = editor.getSelection();
					var range = sel.getRange(true);	// https://developer.mozilla.org/en-US/docs/Web/API/Range
					if (!isHtmlMode && (editor.get_html(true, false) != "")) {	// design mode containing content only
						if (!range.setStart) { // IE7/8 textRange
							//console.log("IE7/8 textRange");
							// IE 7 is not supported and 8 has issues with pasting previews and content in place so we are always pasting the script text at the beginning of the editor
							// IE 8 forces you to move the cursor out of the editor before the drop occurs.  It also improperly displays the dragging element.  If these problems are reported we may just want to find a way to display a message to IE 8 users that the experience will be improved by upgrading their browser.
							if (args.type === "scriptbuilder") {
								if (CpEditor_EnableCpScriptingFilter == 1)
									html = $(html).html();
								else if (CpEditor_EnableCpScriptingFilter == 2)
									html = $(html).find(".cpsys_ScriptSource").html();
							}

							var $content = $(editor.get_contentArea());
							if ($content.find(".cpsys_Script").length > 0)
								$content.html(html + editor.get_html(true, false));
							else
								$content.prepend(html);
							return;
						} else if (CpEditor_EnableCpScriptingFilter > 0) {	// previews or script highlighting only
							// we cannot insert a script within a script, only if the entire script is being replaced
							var $script = $(range.commonAncestorContainer).closest(".cpsys_Script");
							if ($script.length == 1) {	// cursor is in a script
								if (range.collapsed) {
									//$(args.html).insertBefore($script);	// 2015-07-08: This line was replaced with the line below because it did not work when args.html contained simple text as in the Toolbox module.
									$script.before(html);	// if the cursor is in the script insert the content before the script
									return;
								} else {
									$script.replaceWith(html);	// if a selection of text is made within the script replace the entire script
									return;
									//editor.selectElement($script[0]);	
								}
							}
						}
					} else if (isHtmlMode && document.all && document.addEventListener) {	// html mode in IE9/10: http://tanalin.com/en/articles/ie-version-js/
						// IE9/10 sometimes paste the html content above the text area so we are just always appending it at the end of the text area.  This will prevent you from selecting the location of your paste in HTML mode in IE9/10.
						//console.log(editor.get_textArea());
						$(editor.get_textArea()).append(html);
						return;
					}

					editor.pasteHtml(html);
				}

				var type = $tool.data("type");
				if (type == "scriptbuilder") {
					editor.showExternalDialog(sbUrl + "&istext=" + (isHtmlMode ? "1" : "0"), source, sbWidth, sbHeight, pasteHtml, null, "Script Designer", true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
				} else {
					var arguments = {};
					arguments.html = source;
					arguments.type = type;
					pasteHtml(editor, arguments);
				}
			} else {
				if ($tool.data("type") == "scriptbuilder") {
					sbDialog.argument = { script: source, destination: $destination };
					//console.log("show", sbUrl);
					sbDialog.setUrl(sbUrl);
					sbDialog.show();
				}
				else {
					pasteText($destination, source);
				}
			}
		}
	};
	$(".ConsoleFormInput input:text, .ConsoleFormInput textarea:not(.reTextArea), .ConsoleFormInput .RadEditor").droppable(toolboxDroppable); // changes to this selector must also be made to Controls.js: CpAttributeSwitch_ExecutePlugins
}