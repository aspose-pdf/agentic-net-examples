using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string flattenedPath = "flattened.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Flatten annotations using PdfAnnotationEditor ----------
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);                 // Load the PDF
        editor.FlatteningAnnotations();            // Flatten all annotations
        editor.Save(flattenedPath);                // Save the flattened PDF
        editor.Close();                            // Release resources

        // ---------- Validate that no annotations remain ----------
        using (Document doc = new Document(flattenedPath))
        {
            bool anyAnnotations = false;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection may be null; check Count if present
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
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