using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Load PDF into PdfAnnotationEditor (Facades API)
        // -------------------------------------------------
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // -------------------------------------------------
        // 2. Flatten all annotations in the document
        // -------------------------------------------------
        editor.FlatteningAnnotations();

        // -------------------------------------------------
        // 3. Save the flattened PDF
        // -------------------------------------------------
        editor.Save(outputPath);
        editor.Close(); // Dispose the bound document

        // -------------------------------------------------
        // 4. Verify that no annotation objects remain
        // -------------------------------------------------
        using (Document doc = new Document(outputPath))
        {
            bool anyAnnotations = false;

            foreach (Page page in doc.Pages)
            {
                // Annotations collection is 1‑based; Count == 0 means none
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {page.Number} still contains {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
                Console.WriteLine("Validation succeeded: No annotations present after flattening.");
            else
                Console.WriteLine("Validation failed: Some annotations were not flattened.");
        }
    }
}