using System;
using System.IO;
using System.Linq;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Remove document‑level JavaScript (JavaScriptCollection)
            // ------------------------------------------------------------
            if (doc.JavaScript != null)
            {
                // JavaScriptCollection does not provide a Clear() method.
                // Remove each script entry individually via its key.
                List<string> keys = doc.JavaScript.Keys.ToList();
                foreach (string key in keys)
                {
                    doc.JavaScript.Remove(key);
                }
            }

            // ------------------------------------------------------------
            // 2. Remove page‑level actions (only OnOpen / OnClose exist)
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                if (page.Actions != null)
                {
                    page.Actions.OnOpen = null;
                    page.Actions.OnClose = null;
                }
            }

            // ------------------------------------------------------------
            // 3. Remove actions from every form field (WidgetAnnotation)
            // ------------------------------------------------------------
            foreach (Field field in doc.Form.Fields)
            {
                if (field is WidgetAnnotation widget)
                {
                    var actions = widget.Actions;
                    if (actions != null)
                    {
                        // All possible JavaScript‑related actions are set to null
                        actions.OnEnter = null;
                        actions.OnExit = null;
                        actions.OnPressMouseBtn = null;
                        actions.OnReleaseMouseBtn = null;
                        actions.OnReceiveFocus = null;
                        actions.OnLostFocus = null;
                        actions.OnOpenPage = null;
                        actions.OnClosePage = null;
                        actions.OnShowPage = null;
                        actions.OnHidePage = null;
                        actions.OnModifyCharacter = null;
                        actions.OnValidate = null;
                        actions.OnFormat = null;
                        actions.OnCalculate = null;
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
