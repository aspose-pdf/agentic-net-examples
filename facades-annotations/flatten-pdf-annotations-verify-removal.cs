using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string flattenedPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Flatten all annotations using PdfAnnotationEditor (facade API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(); // removes annotation objects
            editor.Save(flattenedPath);
        }

        // Verify that the resulting PDF contains no annotation objects
        using (Document doc = new Document(flattenedPath))
        {
            bool anyAnnotations = false;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still has {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
            {
                Console.WriteLine("Validation passed: No annotations found after flattening.");
            }
            else
            {
                Console.WriteLine("Validation failed: Some annotations remain.");
            }
        }
    }
}