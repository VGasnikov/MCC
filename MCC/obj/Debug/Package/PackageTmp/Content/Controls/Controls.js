/// <reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.0-vsdoc.js" />

function CpAttributeSwitch_ExecutePlugins(clientId, isAsyncPostback) {
	//console.log("CpAttributeSwitch_ExecutePlugins");
	if ((typeof toolboxDroppable !== 'undefined') && !!toolboxDroppable) {
		$(".ConsoleFormInput input:text, .ConsoleFormInput textarea:not(.reTextArea), .ConsoleFormInput .RadEditor").droppable(toolboxDroppable);
	}
	if (isAsyncPostback) {
		setTimeout(function () {    // setTimeout waits for the editors to load before setting the focus
			$('#' + clientId + ' :checked').focus();
		}, 0);
	}
}

function CpLengthValidatorIsValid(val) {
    var value = ValidatorGetValue(val.controltovalidate);
    if (ValidatorTrim(value).length == 0) return true;
    if (val.maximumlength < 0) return true;
    return (value.length <= val.maximumlength);
}

function cpAudienceSelector_CheckChanged(source, clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams) {
    var hiddenField = $get(hiddenFieldId);
    var checkbox = $get('cbx' + clientId);
    var checkbox2 = $get('cbx' + clientId + '2');

    hiddenField.value = hiddenField.value.toLowerCase();
    var values = hiddenField.value.split(', ');

    var params = addParams.split('|');
    var forceRoot = ((params.length >= 3) && (params[2] == 'true'));
    var uncheckDependency = ((params.length >= 4) && (params[3] == 'true'));
    if (checkbox2 != null) {
        if (uncheckDependency) {
            if ((source.id == checkbox2.id) && !checkbox.disabled && !checkbox.checked && checkbox2.checked) checkbox.checked = true; 	// when checkbox 2 is checked make sure checkbox 1 is also checked
            if ((source.id == checkbox.id) && !checkbox2.disabled && !checkbox.checked && checkbox2.checked) checkbox2.checked = false; // when checkbox 1 is unchecked make sure checkbox 2 is also unchecked
        } else {
            if ((source.id == checkbox.id) && !checkbox2.disabled && checkbox.checked && !checkbox2.checked) checkbox2.checked = true; 	// when checkbox 1 is checked make sure checkbox 2 is also checked
        }
    } else {
        checkbox2 = checkbox;
    }

    var all = 'bf7bb52f-eae7-4d5a-bd20-6849d0260c80', root = '5f2d099d-a0df-4e62-bb88-93662c972ae0';
    if (checkbox.value == all) {
        var divRoot = $get('div' + clientId.replace(all, root));
        if (divRoot != null) {
            if (checkbox.checked && checkbox2.checked) {
                hiddenField.value = all + ':1:1';
                if (forceRoot) {
                    hiddenField.value += ', ' + root + ':0:1';
                    var cbxRootPublish = $get(checkbox.id.replace(all, root));
                    if (cbxRootPublish != null) cbxRootPublish.checked = false;
                }
                divRoot.style.display = 'none';
                cpTieredSelector_ResizeDiv(clientId.substring(0, clientId.indexOf('__input')));
            } else {
                hiddenField.value = (checkbox.checked || checkbox2.checked) ? checkbox.value + ':' + cpAudienceSelector_ConvertBool(checkbox.checked) + ':' + cpAudienceSelector_ConvertBool(checkbox2.checked) : '';
                for (var i = 0; i < values.length; i++) {
                    var len = values[i].trim().length;
                    if (len >= 36) {
                        var id = values[i].substring(0, 36).toLowerCase();
                        if (id != checkbox.value.toLowerCase()) {
                            var publish = (len >= 38) ? values[i].substring(37, 38) : '0';
                            if (checkbox.checked) publish = '0';
                            var edit = (len >= 40) ? values[i].substring(39, 40) : '0';
                            if (forceRoot && (id == root)) edit = '1'; else if (checkbox2.checked) edit = '0';
                            if (hiddenField.value != '') hiddenField.value += ', ';
                            hiddenField.value += (id + ':' + publish + ':' + edit);
                        }
                    }
                }
                divRoot.style.display = 'block';
                var cbxRootPublish = $get(checkbox.id.replace(all, root));
                if (cbxRootPublish != null) {
                    cbxRootPublish.disabled = checkbox.checked;
                    if (checkbox.checked) cbxRootPublish.checked = false;
                }
                Centralpoint.Web.UI.WebServices.TieredSelector.Render(index, success, tableName, textField, valueField, parentField, root, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, hiddenField.value, false, enabled, addParams, eval(success));
            }
        }
    } else {
        var allPublish = false; var allEdit = false;
        hiddenField.value = '';
        for (var i = 0; i < values.length; i++) {
            var len = values[i].trim().length;
            if (len >= 36) {
                var id = values[i].substring(0, 36);
                if (id != checkbox.value.toLowerCase()) {
                    if (hiddenField.value != '') hiddenField.value += ', ';
                    hiddenField.value += values[i];
                    if (id == all) {
                        allPublish = (len >= 38) ? (values[i].substring(37, 38) == '1') : true;
                        allEdit = (len >= 40) ? (values[i].substring(39, 40) == '1') : true;
                    }
                }
            }
        }

        var publish = (allPublish) ? '0' : cpAudienceSelector_ConvertBool(checkbox.checked);
        var edit = (allEdit) ? '0' : cpAudienceSelector_ConvertBool(checkbox2.checked);
        if (forceRoot && (checkbox.value.toLowerCase() == root)) edit = '1';
        if ((publish == '1') || (edit == '1')) {
            if (hiddenField.value != '') hiddenField.value += ', ';
            hiddenField.value += (checkbox.value + ':' + publish + ':' + edit);
        }
    }

    cpTieredSelector_CheckChangedBase(source, clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams);
}

function cpAudienceSelector_ConvertBool(value) {
    if (value) return '1'; else return '0';
}

function cpPagedListBox_CheckChanged(source, hiddenFieldId, countLinkId, isSingle) {
    var hiddenField = $get(hiddenFieldId);

    if (isSingle) {
        hiddenField.value = source.value;
    } else {
        hiddenField.value = hiddenField.value.toLowerCase();
        var values = hiddenField.value.split(', ');
        hiddenField.value = source.checked ? source.value : '';
        for (var i = 0; i < values.length; i++) {
            if ((values[i].trim() != '') && (values[i] != source.value.toLowerCase())) {
                if (hiddenField.value != '') hiddenField.value += ', ';
                hiddenField.value += values[i];
            }
        }
    }

    cpPagedListBox_SetSelectedItemCount(hiddenField, countLinkId);
}

function cpPagedListBox_CheckAll(source, hiddenFieldId, countLinkId, controlId) {
    var hiddenField = $get(hiddenFieldId);
    var control = $get(controlId);
    var groupName = controlId + '_Selector';

    hiddenField.value = hiddenField.value.toLowerCase();
    var checkboxes = control.getElementsByTagName('input');
    if (source.checked) {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].name == groupName) {
                checkboxes[i].checked = true;
                if (hiddenField.value.indexOf(checkboxes[i].value.toLowerCase()) < 0) {
                    if (hiddenField.value != '') hiddenField.value += ', ';
                    hiddenField.value += checkboxes[i].value;
                }
            }
        }
    } else {
        var unchecked = '';
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].name == groupName) {
                checkboxes[i].checked = false;
                if (unchecked != '') unchecked += ', ';
                unchecked += checkboxes[i].value;
            }
        }
        unchecked = unchecked.toLowerCase();
        var values = hiddenField.value.split(', ');
        hiddenField.value = '';
        for (var i = 0; i < values.length; i++) {
            if ((values[i].trim() != '') && (unchecked.indexOf(values[i]) < 0)) {
                if (hiddenField.value != '') hiddenField.value += ', ';
                hiddenField.value += values[i];
            }
        }
    }

    //alert(hiddenField.value);
    cpPagedListBox_SetSelectedItemCount(hiddenField, countLinkId);
}

function cpPagedListBox_SetSelectedItemCount(hiddenField, countLinkId) {
    var countLink = $get(countLinkId);

    if (countLink != null) {
        //var count = Math.ceil(hiddenField.value.length / 38);
        var count = (hiddenField.value.length <= 0) ? 0 : hiddenField.value.split(",").length;
        countLink.innerHTML = count + ' Selected Item'
        if (count != 1) countLink.innerHTML += 's';
        if (count <= 0)
            $(countLink).on('click', function () { return false; });
        else
            $(countLink).off('click');
    }
}

function cpPagedListBox_OnClientLoad(clientId, scrollDivId, maxHeight) {
    var scrollDiv = $get(scrollDivId);
    scrollDiv.style.height = (scrollDiv.scrollHeight > maxHeight) ? maxHeight + 'px' : 'auto';
    $('#' + clientId).find('a, .CpPagedListBoxSearch input, .CpPagedListBoxSearch select').attr('tabindex', '-1');
}

function cpPagedListBox_ToggleProgressIndicator(progressDivId, display) {
    var progressDiv = $get(progressDivId);
    if (progressDiv != null) progressDiv.style.display = display ? 'block' : 'none';
}

function CpUploadOpenDialog(type, subFolder, fieldId, previewId, title, isConsole, dialog) {
    if (typeof isConsole == "undefined") isConsole = false;
    if (typeof dialog == "undefined") dialog = '00';
    var width = 720; //(type == 'Resource') ? 640 : 387;
    var height = 480; //(type == 'Resource') ? 480 : 259;
    var xpos = (screen.width - width) / 2;
    var ypos = (screen.height - height) / 2;
    var prefix = isConsole ? '/Console' : '';
    // the standard file upload url cannot always contain /Console/ because that would break it in the master and uber consoles
    var url = (type == 'Resource')
		? '/Console/Resources.aspx?sn=Resources&type=control&fieldid=' + fieldId + '&previewid=' + previewId + '&title=' + title + '&debug=noupdatepanels'
		: prefix + '/Integrations/Centralpoint/FileUpload/Default.aspx?type=' + type + '&subfolder=' + subFolder + '&fieldid=' + fieldId + '&previewid=' + previewId + '&title=' + title + '&dialog=' + dialog;
    var newWindow = window.open(url, 'FileUpload_' + fieldId, 'menubar=no,status=no,scrollbars=yes,resizable=yes,height=' + height + ',width=' + width + ',left=' + xpos + ',top=' + ypos + ',screenX=' + xpos + ',screenY=' + ypos + ',toolbar=no,location=no,directories=no');
}

function CpUploadUpdatePreview(fieldId, previewId, ext, isConsole, isResource) {
    var field = document.getElementById(fieldId);
    var preview = document.getElementById(previewId);
    if (typeof isConsole == "undefined") isConsole = false;
    if (typeof isResource == "undefined") isResource = false;
    var url = field.value;
    if (url == '') {
        preview.style.display = 'none';
    }
    else if (url.startsWith('http://')) {
        preview.innerHTML += "<br />";
        preview.innerHTML += "<img src=\"" + url + "\" alt=\"" + url + "\" style=\"padding: 4px 0px 4px 0px;\" />";
        preview.style.display = 'block';
    }
    else {
        if (typeof ext == "undefined") {
            ext = url.substring(url.length - 4).toLowerCase();
            if (ext.charAt(0) == '.') ext = ext.substring(1);
        }

        var prefix = (isConsole && isResource) ? '/Console' : '';
        preview.innerHTML = "<a href=\"" + prefix + url + "\" target=\"_blank\">" + url + "</a>";
        switch (ext) {
            case "jpg":
            case "jpeg":
            case "gif":
            case "png":
                preview.innerHTML += "<br />";
                if (isResource)
                    preview.innerHTML += "<img src=\"" + prefix + url + "&width=600\" alt=\"" + url + "\" style=\"padding: 4px 0px 4px 0px;\" />";
                else
                    preview.innerHTML += "<img src=\"" + prefix + url + "/600/0/false/Image.ashx\" alt=\"" + url + "\" style=\"padding: 4px 0px 4px 0px;\" />";
                break;
            default:
                break;
        }
        preview.style.display = 'block';
    }
}

if (typeof (Sys) != 'undefined') Sys.Application.notifyScriptLoaded();  // 2012-07-11: this line was formerly part of CpUpload.js but I have no idea what it does

function CpEditor_ResourceManager(commandName, editor, oTool) {
    //editor.showDialog('Media', {}, function (sender, args) { editor.pasteHtml(args.html); });
    var callbackFunction = function(sender, args) { editor.pasteHtml(args.html); }
    //showExternalDialog(url, argument, width, height, callbackFunction, callbackArgs, title, modal, behaviors, showStatusbar, showTitlebar);
    editor.showExternalDialog('/Console/RtfEditorDialog.aspx?view=Media', null, 640, 480, callbackFunction, null, 'Media Manager', true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
}

function CpEditor_WebSiteLinks(commandName, editor, oTool) {
    //editor.showDialog('Links', {}, function (sender, args) { editor.pasteHtml(args.html); });
    var callbackFunction = function (sender, args) { editor.pasteHtml(args.html); }
    //showExternalDialog(url, argument, width, height, callbackFunction, callbackArgs, title, modal, behaviors, showStatusbar, showTitlebar);
    editor.showExternalDialog('/Console/RtfEditorDialog.aspx?view=Links', null, 640, 480, callbackFunction, null, 'Site Links', true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
}

function CpEditor_ScriptDesigner(commandName, editor, oTool) {
	//console.log("CpEditor_ScriptDesigner", elem);
	var script = null;
	var preHtml = "", postHtml = "";
	var elem = editor.getSelectedElement();
	if (elem && (elem.tagName === "CODE")) {
		$elem = $(elem);
		if ($elem.data("generator") === "CpEditor") {
			editor.selectElement(elem);
			script = $elem.html();
			//console.log(elem, $elem[0]);

			var start = script.indexOf('[');
			if (start > 0) {
				preHtml = script.substring(0, start);
				script = script.substring(start);
				//console.log(start, preHtml, script);
			}
			var end = script.indexOf("/]") + 2;
			if (end < script.length) {
				postHtml = script.substring(end);
				script = script.substring(0, end);
				//console.log(end, preHtml, script);
			}
			//console.log(script);
		}
	}

	var callbackFunction = function (sender, args) {
		editor.pasteHtml(preHtml + args.html + postHtml);
	}

	var module = "Template";
	var views = "";
	if (typeof (cpsys) !== "undefined") {
		if (typeof (cpsys.SiteMapInfo) !== "undefined")
			module = cpsys.SiteMapInfo.SystemName;
		if (typeof (cpsys.ModuleViewsInfo) !== "undefined")
			views = cpsys.ModuleViewsInfo.NavigationItemId + ':' + cpsys.ModuleViewsInfo.GroupId;
	}

	var width = 800, height = 600
	if ((window.innerWidth > 1024) && (window.innerHeight > 768)) { width = 1024; height = 768; }
	var url = "/Console/ScriptBuilder/Dialog.aspx?module=" + module + "&views=" + views;
	if (script != null) url += "&edit=1";
	editor.showExternalDialog(url, script, width, height, callbackFunction, null, "Script Designer", true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
}

function CpEditor_OnClientPasteHtml(sender, args) {
	var commandName = args.get_commandName();
	if ((commandName == "InsertTable") || (commandName == "TableWizard")) {
        var div = document.createElement("DIV");
        div.innerHTML = args.get_value();
        var table = div.firstChild;
        if (table.cellSpacing == '') table.cellSpacing = '0';
        if (table.cellPadding == '') table.cellPadding = '0';
        table.className = 'cpsys_Table';
        args.set_value(div.innerHTML);
	}
	else if (commandName == "MediaManager") {  //adding support for HTML5 ~JZ~ 9/9/15
		var value = args.get_value();
		var width = $(value).attr('width');
		var height = $(value).attr('height');
		var fileName = $(value).find('param').attr('value');
		var ext = fileName.substring(fileName.indexOf('.') + 1).toLowerCase();
		//alert(width + "|" + height + "|" + fileName + "|" + ext);

		var div = document.createElement("DIV");
		if (ext == "mp4" || ext == "webm" || ext == "ogv") {
			if (ext == "ogv") {
				ext = "ogg"
			}

			var video = "<video width='" + width + "' height='" + height + "' controls='controls'>";
			video += "<source src='" + fileName + "' type='video/" + ext + "' />";
			//html+="<!-- fallback to Flash: -->";
			//video += "<object width='" + width + "' height='" + height + "' type='application/x-shockwave-flash' data='" + fileName + ">";
			//video += "<param name='movie' value='" + fileName + "' />";
			//video += "<param name='flashvars' value='autostart=true&amp;controlbar=over&amp;file=" + fileName + "' />";
			//video += "Your browser does not support this video format, please <a href='" + fileName + "'>download the video</a>' to view. Please consider downloading a more recent browser!";
			//video += "</object>";
			video += "Your browser does not support this video format, please <a href='" + fileName + "'>download the video</a>' to view. Please consider downloading a more recent browser!";
			video += "</video>";
			args.set_value(video);
		} else if (ext == "mp3" || ext == "ogg" || ext == "wav") {
			if (ext == "mp3") {
				ext = "mpeg";
			}
			//for audio can support mp3, ogg, and wav - audio/mpeg, audio/ogg, audio/wav 
			var audio = "<audio controls>";
			audio += "<source src='" + fileName + "' type='audio/" + ext + "' controls='controls'>";
			audio += "Your browser does not support the audio element. Please consider downloading a more recent browser.";
			audio += "</audio>";
			//div.innerHTML = audio;
			//args.set_value(div.innerHTML);
			args.set_value(audio);
		} else {
			div.innerHTML = value;
		}
	}
	else if ((CpEditor_EnableCpScriptingFilter > 0) && (commandName == "Paste")) {
		var value = args.get_value();
		if (value.search(/\[cp:/ig) >= 0) {
			//console.log(value);
			var $replacement = $("<div />").html(value);
			var setValue = false;
			// remove spans created by Chrome from scripting code elements
			$replacement.find("span").each(function (index) {
				var $span = $(this);
				if ($span.css("font-family") === "courier, monospace") {
					var html = $span.html();
					//console.log(html);
					$span.replaceWith(html);
					setValue = true;
				}
			});
			// remove fonts created by IE from scripting code elements
			$replacement.find("font[face='Courier New']").each(function (index) {
				var $font = $(this);
				var html = $font.html();
				//console.log(html);
				$font.replaceWith(html);
				setValue = true;
			});
			if (setValue) args.set_value($replacement.html());
		}
	}
}

function CpEditor_OnClientLoad(editor, args) {
	// This method is only called in consoles.  Root/Modules/Forms/Editor.aspx calls Editor.js/onClientLoad which has to be modified independently.

	//CpEditor_EnableCpScriptingFilter = 1;
	var filtersManager = editor.get_filtersManager();
	filtersManager.add(new CpEditor_RemoveLastBrFilter());

	if (CpEditor_EnableCpScriptingFilter > 0) {
		filtersManager.add(new CpEditor_CpScriptingFilter());
		editor.set_html(editor.get_html());  // trigger the getDesignContent filters on the initial page load, get_html(true) would also trigger the getHtmlContent filters, this does not execute the custom filters added below

		var contextMenu = editor.getContextMenuByTagName("CODE");
		if (!contextMenu) return;
		contextMenu.add_show(function () {
			var $sel = $(editor.getSelectedElement());
			if (!$sel.hasClass("cpsys_Script")) {
				var menuItems = contextMenu.get_items();
				menuItems[0].remove();	// the script designer is the only item on the element
			}
		});
	}

	if (typeof CpEditor_Custom_OnClientLoad === 'function') CpEditor_Custom_OnClientLoad(editor, args);

	if (editor.getToolByName("ScriptDesigner")) {
		$editor = $(editor.get_mainTable());
		//console.log($editor.closest(".ConsoleFormInput").data("allowscripts"));
		if (!$editor.closest(".ConsoleFormInput").data("allowscripts")) {
			//console.log($editor.find("span.ScriptDesigner"));
			$editor.find("span.ScriptDesigner").css({ opacity: 0.4 }).attr("title", "Not Supported");
		}
	}

	$('.reToolCell a, .reToolZone a').attr('tabindex', '-1');
}

function CpEditor_OnClientSelectionChange(editor, args) {
	if (CpEditor_EnableCpScriptingFilter < 2) return;
	//console.log("CpEditor_OnClientSelectionChange", editor._clientStateFieldID, args);

	var sel = editor.getSelection();
	var range = sel.getRange(true);	// https://developer.mozilla.org/en-US/docs/Web/API/Range
	if (!range) return;

	if (!range.setStart) { // IE7/8 textRange
		//console.log("IE8 Click", sel, range, sel.getBrowserSelection());
		if (sel.getBrowserSelection().type == "None") return; // prevent the initial browser load from triggering the replacement
		var $content = $(editor.get_contentArea());
		if ($content.find(".cpsys_Script").length > 0) {
			//console.log("IE8 Click", range, $content);
			$content.html(editor.get_html(true, false));
		}
		return;
	}

	var scriptSelector = "div.cpsys_Script";
	var $container = $(range.commonAncestorContainer).closest(scriptSelector);
	//console.log("$container", $container);
	if ($container.length == 1) {
		//console.log('Common Ancestor Script');
		var $replacement = CpEditor_ReplaceScriptPreview($container);
		//console.log($replacement[0]);
		editor.selectElement($replacement[0]);
	} else if (!range.collapsed) {
		//console.log("range", range);
		var selectRange = false;
		var $start = $(range.startContainer).closest(scriptSelector);
		if ($start.length == 1) {
			//console.log('Start In Script')
			var $replacement = CpEditor_ReplaceScriptPreview($start);
			range.setStartBefore($replacement[0]);
			selectRange = true;
		}
		var $end = $(range.endContainer).closest(scriptSelector);
		if ($end.length == 1) {
			//console.log('End In Script')
			var $replacement = CpEditor_ReplaceScriptPreview($end);
			range.setEndAfter($replacement[0]);
			selectRange = true;
		}
		if (selectRange) sel.selectRange(range);
	}
}

function CpEditor_ReplaceAllScriptPreviews(content) {
	//var result = content.replace(/(<|&lt;)div data-cpscript="open"([\s\S]*?)(<|&lt;)div data-cpscript="close" data-generator="CpEditor" style="display:\s*none;" contenteditable="false" unselectable="on"(>|&gt;)\/CpScript(<|&lt;)\/div(>|&gt;)/ig, function (match) {
	var result = content.replace(/(<|&lt;)div .*?data-cpscript="open".*?(>|&gt;)([\s\S]*?)(<|&lt;)div .*?data-cpscript="close".*?(>|&gt;)\/CpScript(<|&lt;)\/div(>|&gt;)/ig, function (match) {
		var $script = $("<div />").html(match);
		var $source = $script.find(".cpsys_ScriptSource");
		return ($source.length == 1) ? $source.html() : match;	// if a script contains more or less than 1 source element there must have been a partial selection or other problem and no change is made
	});
	result = result.replace(/(<|&lt;)code .*?data-generator="CpEditor".*?(>|&gt;)([\s\S]*?)(<|&lt;)\/code(>|&gt;)/ig, "$3");
	return result;
}

function CpEditor_ReplaceScriptPreview($script) {
	var $next = $script.next();
	if ($next.data("cpscript") == "close") $next.remove();

	var $source = $script.find(".cpsys_ScriptSource").css("display", "inline");
	if ($source.length == 1) {
		html = "<code class=\"cpsys_Script\" data-generator=\"CpEditor\">" + $source.html() + "</code>";
	} else {
		html = $script.html();
	}

	var $replacement = $(html);
	$script.replaceWith($replacement);
	return $replacement;
}

function cpTieredSelector_Change(source, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams) {
    var selectedValue = source.options[source.selectedIndex].value;
    var hiddenField = $get(hiddenFieldId);

    var next = $get(hiddenFieldId + index);
    if (next != null) cpTieredSelector_Remove(next.parentNode.parentNode, index, hiddenFieldId);

    if (selectedValue == '') {
        if (index <= 2) {
            hiddenField.value = '';
        } else {
            var previous = $get(hiddenFieldId + (index - 2)).childNodes[0];
            // when the value is set to nothing make sure that an inaccessible value is not inadvertantly selected
            if (previous.options[previous.selectedIndex].text == 'Inaccessible Value') {
                previous.options[0].selected = true;
                cpTieredSelector_Change(previous, index - 1, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams);
                return;
            }
            hiddenField.value = previous.options[previous.selectedIndex].value;
        }
    } else {
        hiddenField.value = selectedValue;
        Centralpoint.Web.UI.WebServices.TieredSelector.Render(index, success, tableName, textField, valueField, parentField, selectedValue, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, '', false, enabled, addParams, eval(success));
    }

    // remove the inaccessible value node when the selection is changed
    var last = source.length - 1;
    if (last > 0) {
        if (source.options[last].text == 'Inaccessible Value') {
            source.remove(last);
            source.parentNode.style.display = 'none';
        }
    }
    //alert('hiddenField.value: ' + hiddenField.value);
}

function cpTieredSelector_Remove(parentNode, index, hiddenFieldId) {
    var i = index;
    var selectorNode = $get(hiddenFieldId + i);

    while (selectorNode != null) {
        parentNode.removeChild(selectorNode.parentNode);
        i++;
        selectorNode = $get(hiddenFieldId + i);
    }
}

function cpTieredSelector_SelectByValue(parentId, value) {
    //alert(parentId + ' - ' + value);
    var parent = $get(parentId);

    // if the parent control is not available wait for it to become available
    if (parent == null) {
        setTimeout('cpTieredSelector_SelectByValue(\'' + parentId + '\', \'' + value.replace(/'/g, '\\\'') + '\')', 100);
        return;
    }

    var source = parent.childNodes[0];
    var inaccessible = true;
    for (var i = 0; i < source.options.length; i++) {
        var option = source.options[i];
        if (option.value == value) {
            option.selected = true;
            inaccessible = false;
            break;
        }
    }

    // if the item doesn't exist add it to the selector as an inaccessible value
    if (inaccessible) cpTieredSelector_AppendOption(source, 'Inaccessible Value', value, true);
}

function cpTieredSelector_AppendOption(source, text, value, selected) {
    var option = document.createElement('option');
    option.text = text;
    option.value = value;
    option.selected = selected;

    try {
        source.add(option, null); // standards compliant; doesn't work in IE
    }
    catch (ex) {
        source.add(option); // IE only
    }
}

function cpTieredSelector_RenderAfterParent(index, success, tableName, textField, valueField, parentField, parentValue, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, parentId, listSelectionMode, force, enabled, addParams) {
    if (parentId != '') {
        var parent = $get(parentId);

        // if the parent control is not available wait for it to become available
        if (parent == null) {
            setTimeout('cpTieredSelector_RenderAfterParent(' + index + ', \'' + success + '\', \'' + tableName + '\', \'' + textField.replace(/'/g, '\\\'') + '\', \'' + valueField + '\', \''
				+ parentField + '\', \'' + parentValue + '\', \'' + filter.replace(/'/g, '\\\'') + '\', ' + hasAncestorsTable + ', ' + prefixFields + ', ' + useDev + ', \'' + hiddenFieldId + '\', \''
				+ parentId + '\', \'' + listSelectionMode + '\', ' + force + ', ' + enabled + ', \'' + addParams + '\')', 100);
            return;
        }
    }
    setTimeout('Centralpoint.Web.UI.WebServices.TieredSelector.Render(' + index + ', \'' + success + '\', \'' + tableName + '\', \'' + textField.replace(/'/g, '\\\'') + '\', \'' + valueField + '\', \''
		+ parentField + '\', \'' + parentValue + '\', \'' + filter.replace(/'/g, '\\\'') + '\', ' + hasAncestorsTable + ', ' + prefixFields + ', ' + useDev + ', \'' + hiddenFieldId + '\', \''
		+ listSelectionMode + '\', \'\', ' + force + ', ' + enabled + ', \'' + addParams + '\', eval(' + success + '))', 100);
}

function cpTieredSelector_Toggle(clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams) {
    var image = $get('img' + clientId);
    var intSep = image.src.lastIndexOf('/') + 1;
    var strSrc = image.src.substring(intSep).toLowerCase();
    if (strSrc != 'plus.gif') {
        image.src = image.src.substring(0, intSep) + 'Plus.gif';
        $get('divc' + clientId).style.display = 'none';
    } else {
        Centralpoint.Web.UI.WebServices.TieredSelector.Render(index, success, tableName, textField, valueField, parentField, $get('cbx' + clientId).value, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, $get(hiddenFieldId).value, false, enabled, addParams, eval(success));
    }
}

function cpTieredSelector_CheckChanged(source, clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams) {
    var hiddenField = $get(hiddenFieldId);
    var checkbox = $get('cbx' + clientId);

    hiddenField.value = hiddenField.value.toLowerCase();
    var values = hiddenField.value.split(', ');
    hiddenField.value = checkbox.checked ? checkbox.value : '';
    for (var i = 0; i < values.length; i++) {
        if ((values[i].trim() != '') && (values[i] != checkbox.value.toLowerCase())) {
            if (hiddenField.value != '') hiddenField.value += ', ';
            hiddenField.value += values[i];
        }
    }

    cpTieredSelector_CheckChangedBase(source, clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams);
}

function cpTieredSelector_CheckChangedBase(source, clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams) {
    var image = $get('img' + clientId);
    var imageSrc = image.src.substring(image.src.lastIndexOf('/') + 1).toLowerCase();

    if (source.checked && (imageSrc == 'plus.gif'))
        cpTieredSelector_Toggle(clientId, index, success, tableName, textField, valueField, parentField, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, selectionMode, enabled, addParams);
}

function cpTieredSelector_ExpandAfterParent(index, success, tableName, textField, valueField, parentField, parentValue, filter, hasAncestorsTable, prefixFields, useDev, hiddenFieldId, listSelectionMode, enabled, addParams) {
    var childrenId = 'divc' + hiddenFieldId + '_' + index + '_' + parentValue;
    var children = $get(childrenId);
    //alert(parentValue + ': ' + (children == null));

    // if the parent control is not available wait for it to become available
    if (children == null) {
        setTimeout('cpTieredSelector_ExpandAfterParent(' + index + ', \'' + success + '\', \'' + tableName + '\', \'' + textField.replace(/'/g, '\\\'') + '\', \'' + valueField + '\', \''
			+ parentField + '\', \'' + parentValue + '\', \'' + filter.replace(/'/g, '\\\'') + '\', ' + hasAncestorsTable + ', ' + prefixFields + ', ' + useDev + ', \''
			+ hiddenFieldId + '\', \'' + listSelectionMode + '\', ' + enabled + ', \'' + addParams + '\')', 100);
        return;
    }

    setTimeout('Centralpoint.Web.UI.WebServices.TieredSelector.Render(' + (index + 1) + ', \'' + success + '\', \'' + tableName + '\', \'' + textField.replace(/'/g, '\\\'') + '\', \'' + valueField + '\', \''
		+ parentField + '\', \'' + parentValue + '\', \'' + filter.replace(/'/g, '\\\'') + '\', ' + hasAncestorsTable + ', ' + prefixFields + ', ' + useDev + ', \'' + hiddenFieldId + '\', \'' + listSelectionMode + '\', \''
		+ $get(hiddenFieldId).value.replace(/'/g, '\\\'') + '\', false, ' + enabled + ', \'' + addParams + '\', eval(' + success + '))', 100);
}

function cpTieredSelector_ShowInaccessibleValuesItem(hiddenFieldId) {
    var item = $get('div' + hiddenFieldId + '_1_[cp:placeholders key=\'InaccessibleValues\' /]');
    if (item == null) {
        setTimeout('cpTieredSelector_ShowInaccessibleValuesItem(\'' + hiddenFieldId + '\')', 100);
        return;
    }
    item.style.display = 'block';
}

function cpTieredSelector_ToggleHeight(source, divId) {
	var div = $get(divId);
	if (source.title == 'Click here to limit the selector\'s height using scroll bars.') {
		div.style.height = '';
		cpTieredSelector_ResizeDiv(divId);
		source.src = '/Integrations/Centralpoint/Resources/Controls/CpTieredSelectorLimited.png';
		source.title = 'Click here to allow the selector to resize to fit it\'s contents.';
	} else {
		div.style.height = 'auto';
		source.src = '/Integrations/Centralpoint/Resources/Controls/CpTieredSelectorDynamic.png';
		source.title = 'Click here to limit the selector\'s height using scroll bars.';
	}
	return false;
}

function cpTieredSelector_ResizeDiv(divId, limit) {
	if (limit == undefined) limit = 152;
	var div = $get(divId);
	if (div != null) {
		if (div.style.height != 'auto')
		{
			var height = div.scrollHeight;
			if (height > limit)
				height = limit;
			else if (height < 15)
				height = 15;
			//alert(div.style.height + ' to ' + height);
			div.style.height = height + 'px';
		}
	}
}

function cpTieredSelector_UpArrows(div) {
	$(div).find("span.CpTieredSelectorMultipleCheckBox-UpArrow").show().on("click", function (e) {
		e.preventDefault();
		//console.log("cpTieredSelector_UpArrows", $(this).siblings("input.CpTieredSelectorMultipleCheckBox:enabled:not(:checked)"));
		//$(this).siblings("input.CpTieredSelectorMultipleCheckBox:enabled:not(:checked)").prop("checked", true);
		$(this).siblings("input.CpTieredSelectorMultipleCheckBox:enabled:not(:checked):first").trigger("click");
		cpTieredSelector_UpArrowsPropagate($(this).parent().parent().parent());
	});
}

function cpTieredSelector_UpArrowsPropagate($div) {
	if ($div.is(".CpTieredSelectorMultiple") || ($div.parents(".CpTieredSelectorMultiple").length <= 0)) return;

	$div.children("input.CpTieredSelectorMultipleCheckBox:enabled:not(:checked):first").trigger("click");
	cpTieredSelector_UpArrowsPropagate($div.parent().parent());
	//console.log("cpTieredSelector_UpArrowsPropagate", $div.parent().parent());
}

function cpListBox_GetSingleValue(source) {
    return source.options[source.selectedIndex].value;
}

function cpListBox_GetMultipleValues(name) {
    var checkbox = eval('document.forms[0].' + name);
    if (checkbox.length == undefined) return checkbox.value;
    var result = '';
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].checked) {
            if (result.length > 0) result += ', ';
            result += checkbox[i].value;
        }
    }
    return result;
}

function cpListBox_GroupSelector(group, checked) {
    for (i = 0; i < group.length; i++) {
        if (!group[i].disabled)
            group[i].checked = checked;
    }
}

function cpRadioButtonList_GetValue(name) {
    var radio = eval('document.forms[0].' + name);
    for (var i = 0; i < radio.length; i++) {
        if (radio[i].checked) return radio[i].value;
    }
}