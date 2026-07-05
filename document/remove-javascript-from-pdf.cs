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

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // ----- Remove document‑level JavaScript actions -----
                // Clear the OpenAction (executed when the document is opened)
                doc.OpenAction = null;

                // Remove any named JavaScript entries stored in the document's JavaScript collection
                if (doc.JavaScript != null)
                {
                    // JavaScriptCollection is not directly enumerable; iterate via Keys
                    var keys = new List<string>(doc.JavaScript.Keys);
                    foreach (var key in keys)
                    {
                        doc.JavaScript.Remove(key);
                    }
                }

                // ----- Remove page‑level JavaScript actions -----
                foreach (Page page in doc.Pages)
                {
                    // Page actions only expose OnOpen and OnClose
                    page.Actions.OnOpen = null;
                    page.Actions.OnClose = null;

                    // ----- Remove JavaScript actions attached to annotations -----
                    // Iterate backwards when removing/modifying items
                    for (int i = page.Annotations.Count; i >= 1; i--)
                    {
                        var annotation = page.Annotations[i];

                        // For link annotations the Action property can hold a JavascriptAction
                        if (annotation is LinkAnnotation link && link.Action is JavascriptAction)
                        {
                            link.Action = null;
                        }
                    }
                }

                // Save the cleaned PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"JavaScript removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
