var scEditor = null;

Telerik.Web.UI.Editor.CommandList["InsertToken"] = function (commandName, editor, args) {
    var url = "/sitecore/shell/default.aspx?xmlcontrol=RichText.InsertToken&la=" + scLanguage;

    scEditor = editor;
    editor.showExternalDialog( url, null, 600, 500,
      scInsertToken, null, "Insert Token", true,
      Telerik.Web.UI.WindowBehaviors.Close, false, false
    );
};

function scInsertToken(sender, returnValue) {
    if (returnValue) {
        scEditor.pasteHtml(returnValue.media);
    }
}