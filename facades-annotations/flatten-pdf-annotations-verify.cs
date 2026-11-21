using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath     = "input.pdf";
        const string flattenedPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Flatten all annotations using the Facades API.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF.
            editor.FlatteningAnnotations();          // Correct method to flatten annotations.
            editor.Save(flattenedPath);              // Save the flattened PDF.
        }

        // Verify that no annotation objects remain by scanning the PDF object tree.
        bool annotationsFound = false;
        using (Document doc = new Document(flattenedPath))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // The Annotations collection is null or empty when no annotations exist.
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    annotationsFound = true;
                    break;
                }
            }
        }

        Console.WriteLine(annotationsFound
            ? "Annotations are still present after flattening."
            : "No annotations found after flattening.");
    }
}
