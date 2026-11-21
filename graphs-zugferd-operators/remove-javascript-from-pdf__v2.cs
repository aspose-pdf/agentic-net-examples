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
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove any document‑level JavaScript actions (OpenAction and possible Actions.JavaScript)
            if (doc.OpenAction is JavascriptAction)
                doc.OpenAction = null;

            // Some Aspose.Pdf versions expose a JavaScript property inside Document.Actions.
            // Use reflection to clear it if it exists and is writable.
            var actions = doc.Actions;
            var jsProp = actions.GetType().GetProperty("JavaScript");
            if (jsProp != null && jsProp.CanWrite)
                jsProp.SetValue(actions, null);

            // 2. Iterate over all pages and delete annotations that contain JavaScript actions.
            foreach (Page page in doc.Pages)
            {
                // Collect annotations to remove (cannot modify collection while iterating)
                List<Annotation> toRemove = new List<Annotation>();

                foreach (Annotation annot in page.Annotations)
                {
                    // Only derived annotation types such as LinkAnnotation expose the Action property.
                    if (annot is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        toRemove.Add(annot);
                    }
                }

                // Delete the collected annotations
                foreach (Annotation jsAnnot in toRemove)
                    page.Annotations.Delete(jsAnnot);
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Clean PDF saved to '{outputPath}'.");
    }
}
