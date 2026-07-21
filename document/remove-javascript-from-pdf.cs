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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove document‑level JavaScript actions -----
            // Clear the document OpenAction (executed when the PDF is opened).
            doc.OpenAction = null;

            // ----- Remove page‑level JavaScript actions -----
            foreach (Page page in doc.Pages)
            {
                // Page actions only expose OnOpen and OnClose.
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // ----- Remove JavaScript actions from annotations -----
            // Iterate all pages and their annotations.
            foreach (Page page in doc.Pages)
            {
                // Annotation collections are 1‑based.
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation annotation = page.Annotations[i];

                    // Only LinkAnnotation (and its subclasses) expose the Action property.
                    if (annotation is LinkAnnotation linkAnnotation)
                    {
                        // If the action is a JavaScript action, clear it.
                        if (linkAnnotation.Action is JavascriptAction)
                        {
                            linkAnnotation.Action = null;
                        }
                    }
                }
            }

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Clean PDF saved to '{outputPath}'.");
    }
}
