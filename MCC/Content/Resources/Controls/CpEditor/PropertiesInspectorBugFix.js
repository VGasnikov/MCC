// Editor Module Manager Tool Properties Inspector Option Fails: http://www.oxcyon.com/Integrations/Centralpoint/Articles/Article.aspx?id=4f2a57b9-045c-4db9-943d-efb61b88d08a
function CpEditor_Custom_OnClientLoad(editor, args) {
	editor.get_modulesManager().getModuleByName("RadEditorNodeInspector").set_visible(false);	// hide the properties/node inspector when Admin > RTF Editor > Editor Modules > Properties Inspector Visible = Yes to create the illusion of No
	//editor.repaint(); // layout issues could require a repaint
}
