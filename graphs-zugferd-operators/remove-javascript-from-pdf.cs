using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class RemoveJavaScript
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove document‑level JavaScript actions -----
            // The Document class exposes a single OpenAction. Setting it to null removes the script.
            doc.OpenAction = null;

            // ----- Remove JavaScript actions from pages -----
            foreach (Page page in doc.Pages)
            {
                // Clear page‑level actions (only OnOpen and OnClose exist in the current SDK).
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;

                // Iterate annotations backwards when we might delete items.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];

                    // Remove embedded file attachments (they are a security risk as well).
                    if (ann is FileAttachmentAnnotation)
                    {
                        page.Annotations.Delete(i);
                        continue;
                    }

                    // Remove JavaScript actions from link annotations.
                    if (ann is LinkAnnotation link && link.Action != null)
                    {
                        if (link.Action is JavascriptAction jsLink && !string.IsNullOrEmpty(jsLink.GetECMAScriptString()))
                        {
                            link.Action = null;
                        }
                    }
                    // Remove JavaScript actions from widget annotations.
                    else if (ann is WidgetAnnotation widget && widget.Actions != null)
                    {
                        ClearWidgetJavaScript(widget);
                    }
                }
            }

            // Save the cleaned PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Saved to '{outputPath}'.");
    }

    // Helper method to clear any JavaScript actions attached to a WidgetAnnotation.
    private static void ClearWidgetJavaScript(WidgetAnnotation widget)
    {
        var actions = widget.Actions;
        if (actions == null) return;

        // List of all possible action properties on AnnotationActionCollection.
        // For each, if it is a JavascriptAction with non‑empty script, set it to null.
        if (actions.OnEnter is JavascriptAction jsEnter && !string.IsNullOrEmpty(jsEnter.GetECMAScriptString()))
            actions.OnEnter = null;
        if (actions.OnExit is JavascriptAction jsExit && !string.IsNullOrEmpty(jsExit.GetECMAScriptString()))
            actions.OnExit = null;
        if (actions.OnPressMouseBtn is JavascriptAction jsPress && !string.IsNullOrEmpty(jsPress.GetECMAScriptString()))
            actions.OnPressMouseBtn = null;
        if (actions.OnReleaseMouseBtn is JavascriptAction jsRelease && !string.IsNullOrEmpty(jsRelease.GetECMAScriptString()))
            actions.OnReleaseMouseBtn = null;
        if (actions.OnReceiveFocus is JavascriptAction jsFocus && !string.IsNullOrEmpty(jsFocus.GetECMAScriptString()))
            actions.OnReceiveFocus = null;
        if (actions.OnLostFocus is JavascriptAction jsLost && !string.IsNullOrEmpty(jsLost.GetECMAScriptString()))
            actions.OnLostFocus = null;
        if (actions.OnOpenPage is JavascriptAction jsOpen && !string.IsNullOrEmpty(jsOpen.GetECMAScriptString()))
            actions.OnOpenPage = null;
        if (actions.OnClosePage is JavascriptAction jsClose && !string.IsNullOrEmpty(jsClose.GetECMAScriptString()))
            actions.OnClosePage = null;
        if (actions.OnShowPage is JavascriptAction jsShow && !string.IsNullOrEmpty(jsShow.GetECMAScriptString()))
            actions.OnShowPage = null;
        if (actions.OnHidePage is JavascriptAction jsHide && !string.IsNullOrEmpty(jsHide.GetECMAScriptString()))
            actions.OnHidePage = null;
        if (actions.OnModifyCharacter is JavascriptAction jsModify && !string.IsNullOrEmpty(jsModify.GetECMAScriptString()))
            actions.OnModifyCharacter = null;
        if (actions.OnValidate is JavascriptAction jsValidate && !string.IsNullOrEmpty(jsValidate.GetECMAScriptString()))
            actions.OnValidate = null;
        if (actions.OnFormat is JavascriptAction jsFormat && !string.IsNullOrEmpty(jsFormat.GetECMAScriptString()))
            actions.OnFormat = null;
        if (actions.OnCalculate is JavascriptAction jsCalc && !string.IsNullOrEmpty(jsCalc.GetECMAScriptString()))
            actions.OnCalculate = null;
    }
}
