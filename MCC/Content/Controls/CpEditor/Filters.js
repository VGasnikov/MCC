/// <reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.0-vsdoc.js" />

CpEditor_RemoveLastBrFilter = function () {
    CpEditor_RemoveLastBrFilter.initializeBase(this);
    this.IsDom = true;
    this.Enabled = true;
    this.Name = "RemoveLastBr";
    this.Description = "Remove the last BR element automatically added by Firefox.";
}

CpEditor_RemoveLastBrFilter.prototype =
{
    getHtmlContent: function (content) {
        if (!content || !$telerik.isFirefox) return content;

        var bodyElement = (content.tagName == "HTML") ? content.getElementsByTagName("BODY")[0] : content;
        var lastChild = (bodyElement.lastChild) ? bodyElement.lastChild : null;
        if (lastChild && lastChild.tagName && "BR" == lastChild.tagName) {
            Telerik.Web.UI.Editor.Utils.removeNode(lastChild);
        }

        var cells = content.getElementsByTagName('TD');
        for (var i = 0; i < cells.length; i++) {
            var cell = cells[i];
            lastChild = (cell.lastChild) ? cell.lastChild : null;
            if (lastChild && lastChild.tagName && "BR" == lastChild.tagName) {
                Telerik.Web.UI.Editor.Utils.removeNode(lastChild);
            }
        }

        return content;
    }
}

CpEditor_RemoveLastBrFilter.registerClass("CpEditor_RemoveLastBrFilter", Telerik.Web.UI.Editor.Filter);

CpEditor_CpScriptingFilter = function () {
    CpEditor_CpScriptingFilter.initializeBase(this);
    this.IsDom = false;
    this.Enabled = true;
    this.Name = "CpScripting";
    this.Description = "Add elements to format CpScripts in design mode and remove them from the html content.";
}

//CpEditor_CpScriptingFilter.prototype =
//{
//    getHtmlContent: function (content) {
//        //console.log('getHtmlContent');
//        //return content.replace(/<code class="cpsys_Script" data-generator="CpEditor">(\[cp:[:|a-z]+\s((?!\/\]).)+\/\])<\/code>/ig, '$1');
//        //return content.replace(/<code class="cpsys_Script" data-generator="CpEditor">(\[cp:[:|a-z]+\s((?!\/\])[\s\S])+\/\])<\/code>/ig, '$1');
//        //return content.replace(/(<|&lt;)code class="cpsys_Script" data-generator="CpEditor"(>|&gt;)(((?!(<|&lt;)\/code(>|&gt;)).)*\[cp:[:|a-z]+\s((?!\/\])[\s\S])+\/\]((?!(<|&lt;)\/code(>|&gt;)).)*)(<|&lt;)\/code(>|&gt;)/ig, '$3');
//        return content.replace(/(<|&lt;)code class="cpsys_Script" data-generator="CpEditor"(>|&gt;)(((?!(<|&lt;)\/code(>|&gt;)).)*)(<|&lt;)\/code(>|&gt;)/ig, '$3');
//    },
//    getDesignContent: function (content) {
//        //console.log('getDesignContent');
//        //return content.replace(/(\[cp:[:|a-z]+\s((?!\/\]).)+\/\])(?!([^<]+)?>)/ig, '<code class="cpsys_Script" data-generator="CpEditor">$1</code>');
//        return content.replace(/(\[cp:[:|a-z]+\s((?!\/\])[\s\S])+\/\])(?!([^<]+)?>)/ig, '<code class="cpsys_Script" data-generator="CpEditor">$1</code>');   // the backward lookup at the end is designed to prevent replacements when the script is inside an html attribute
//    }
//}

CpEditor_CpScriptingFilter.prototype =
{
	getHtmlContent: function (content) {
		//CpEditor_EnableCpScriptingFilter = 1;
		if (content.search(/cpsys_Script/ig) < 0)
			return content;

		if (CpEditor_EnableCpScriptingFilter == 2) {
			return CpEditor_ReplaceAllScriptPreviews(content);
		} else {
			//return content.replace(/(<|&lt;)code class="cpsys_Script cpsys_ScriptHighlighting" data-generator="CpEditor"(>|&gt;)(((?!(<|&lt;)\/code(>|&gt;)).)*)(<|&lt;)\/code(>|&gt;)/ig, "$3");
			return content.replace(/(<|&lt;)code .*?data-generator="CpEditor".*?(>|&gt;)([\s\S]*?)(<|&lt;)\/code(>|&gt;)/ig, "$3");
		}
	},
	getDesignContent: function (content) {
		//CpEditor_EnableCpScriptingFilter = 1;
		if (content.search(/\[cp:/ig) < 0)
			return content;

		if (CpEditor_EnableCpScriptingFilter == 2) {
			var groupId = "", moduleSystemName = "";
			if (typeof (cpsys) !== "undefined") {
				if (typeof (cpsys.ModuleViewsInfo) !== "undefined")
					groupId = cpsys.ModuleViewsInfo.GroupId;
				if (typeof (cpsys.ModuleInfo) !== "undefined")
					moduleSystemName = cpsys.ModuleInfo.SystemName;
			}

			var result = content;
			$.ajax({
				type: "POST",
				url: "/Console/WebServices/ClientMethods.asmx/CpEditorGetDesignContent",
				data: JSON.stringify({ html: content, module: moduleSystemName, view: groupId }),
				dataType: "json",
				contentType: "application/json; charset=utf-8",
				async: false,
				success: function (data) {
					//console.log(data.d);
					result = data.d;
				},
				error: function (xhr, status, error) { /*alert(xhr.responseText);*/  }
			});
			return result;
		} else {
			return content.replace(/(\[cp:[:|a-z]+\s((?!\/\])[\s\S])+\/\])(?!([^<]+)?>)/ig, "<code class=\"cpsys_Script cpsys_ScriptHighlighting\" data-generator=\"CpEditor\">$1</code>");   // the backward lookup at the end is designed to prevent replacements when the script is inside an html attribute
		}
	}
}

CpEditor_CpScriptingFilter.registerClass("CpEditor_CpScriptingFilter", Telerik.Web.UI.Editor.Filter);