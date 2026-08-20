using System;
using System.Collections.Generic;
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

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Remove document‑level JavaScript actions
            // ------------------------------------------------------------
            // Clear the OpenAction (executed when the document is opened)
            doc.OpenAction = null;

            // Remove any named JavaScript entries from the document's JavaScript collection
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                // Copy keys to a list to avoid modifying the collection while iterating
                List<string> keys = new List<string>(doc.JavaScript.Keys);
                foreach (string key in keys)
                {
                    doc.JavaScript.Remove(key);
                }
            }

            // ------------------------------------------------------------
            // 2. Remove page‑level JavaScript actions
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Page actions only expose OnOpen and OnClose
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // ------------------------------------------------------------
            // 3. Remove JavaScript actions attached to annotations
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards because we may delete annotations
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    // Only LinkAnnotation (and its derived types) expose an Action property
                    if (ann is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null;
                    }
                }
            }

            // ------------------------------------------------------------
            // 4. Save the cleaned PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Saved to '{outputPath}'.");
    }
}
