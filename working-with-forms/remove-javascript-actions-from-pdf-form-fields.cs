using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form != null && doc.Form.Fields != null)
            {
                // Iterate over all form fields in the AcroForm
                foreach (Field field in doc.Form.Fields)
                {
                    // All form field classes inherit from WidgetAnnotation, so we can treat them as such
                    if (field is WidgetAnnotation widget && widget.Actions != null)
                    {
                        var actions = widget.Actions;

                        // Remove JavaScript actions from each possible trigger
                        if (actions.OnEnter is JavascriptAction) actions.OnEnter = null;
                        if (actions.OnExit is JavascriptAction) actions.OnExit = null;
                        if (actions.OnPressMouseBtn is JavascriptAction) actions.OnPressMouseBtn = null;
                        if (actions.OnReleaseMouseBtn is JavascriptAction) actions.OnReleaseMouseBtn = null;
                        if (actions.OnReceiveFocus is JavascriptAction) actions.OnReceiveFocus = null;
                        if (actions.OnLostFocus is JavascriptAction) actions.OnLostFocus = null;
                        if (actions.OnOpenPage is JavascriptAction) actions.OnOpenPage = null;
                        if (actions.OnClosePage is JavascriptAction) actions.OnClosePage = null;
                        if (actions.OnShowPage is JavascriptAction) actions.OnShowPage = null;
                        if (actions.OnHidePage is JavascriptAction) actions.OnHidePage = null;
                        if (actions.OnModifyCharacter is JavascriptAction) actions.OnModifyCharacter = null;
                        if (actions.OnValidate is JavascriptAction) actions.OnValidate = null;
                        if (actions.OnFormat is JavascriptAction) actions.OnFormat = null;
                        if (actions.OnCalculate is JavascriptAction) actions.OnCalculate = null;
                    }
                }
            }

            // Additionally, remove any document‑level JavaScript entries (optional but improves security)
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                // Create a copy of the keys because we will modify the collection while iterating
                List<string> keys = new List<string>(doc.JavaScript.Keys);
                foreach (string key in keys)
                {
                    // Remove the script associated with the key
                    doc.JavaScript.Remove(key);
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
