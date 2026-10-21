using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cloned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        CloneAndModifyAnnotation(inputPath, outputPath);
        Console.WriteLine($"Cloned annotation saved to '{outputPath}'.");
    }

    // Clones the first annotation on page 1, modifies its properties,
    // and adds the cloned annotation back to the same page using the current Aspose.Pdf API.
    static void CloneAndModifyAnnotation(string pdfPath, string resultPath)
    {
        // Load the PDF document (no Facades API needed for annotation handling)
        Document doc = new Document(pdfPath);

        // Pages are 1‑based in Aspose.Pdf
        Page page = doc.Pages[1];
        var annotations = page.Annotations;

        if (annotations == null || annotations.Count == 0)
        {
            Console.WriteLine("No annotations found on page 1.");
            // Save the original PDF unchanged
            doc.Save(resultPath);
            return;
        }

        // Take the first annotation as an example
        Annotation original = annotations[0];

        // Handle TextAnnotation (sticky‑note) cloning – other types can be added similarly
        if (original is TextAnnotation txt)
        {
            // Correct constructor: TextAnnotation(Page page, Rectangle rect)
            TextAnnotation clone = new TextAnnotation(page, txt.Rect)
            {
                Title    = "Cloned Annotation",
                Contents = "This annotation was cloned and modified.",
                Open     = txt.Open,
                Icon     = txt.Icon
            };

            // Add the cloned annotation back to the same page
            page.Annotations.Add(clone);
        }
        else
        {
            // For non‑TextAnnotation types, a generic clone can be created by copying the underlying dictionary.
            // This example simply reports the unsupported type.
            Console.WriteLine($"Annotation type '{original.GetType().Name}' is not handled.");
        }

        // Persist the changes
        doc.Save(resultPath);
    }
}
