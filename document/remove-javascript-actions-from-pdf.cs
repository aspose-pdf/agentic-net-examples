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

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // ---------- Remove document‑level JavaScript actions ----------
            // The document‑level JavaScript is stored in the OpenAction property.
            doc.OpenAction = null;

            // ---------- Remove page‑level JavaScript actions ----------
            foreach (Page page in doc.Pages)
            {
                // Page actions are stored in a PageActionCollection.
                page.Actions.OnOpen = null;   // clear the open action
                page.Actions.OnClose = null;  // clear the close action

                // ---------- Remove annotation JavaScript actions ----------
                foreach (Annotation ann in page.Annotations)
                {
                    // Only LinkAnnotation (and its derived types) expose the Action property.
                    if (ann is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null; // detach the JavaScript action
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
