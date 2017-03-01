// this method is required but its contents can be changed
function CpEditor_Custom_OnClientLoad(editor, args) {
    editor.get_filtersManager().add(new CpEditor_Custom_Filter());
}

CpEditor_Custom_Filter = function () {
    CpEditor_Custom_Filter.initializeBase(this);
    this.IsDom = false;
    this.Enabled = true;
    this.Name = "Custom Filter";
    this.Description = "Apply custom changes to the editor content.";
}

CpEditor_Custom_Filter.prototype =
{
    getHtmlContent: function (content) {
        var newContent = content;
        newContent = newContent.toUpperCase();
        return newContent;
    },
    getDesignContent: function (content) {
        var newContent = content;
        newContent = newContent.toLowerCase();
        return newContent;
    }
}

CpEditor_Custom_Filter.registerClass("CpEditor_Custom_Filter", Telerik.Web.UI.Editor.Filter);