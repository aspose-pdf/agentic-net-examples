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
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Flatten all annotations using the PdfAnnotationEditor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.FlatteningAnnotations(); // removes annotation objects by flattening them
        editor.Save(outputPath);
        editor.Close(); // release resources held by the facade

        // Verify that the resulting PDF contains no annotation objects
        using (Document doc = new Document(outputPath))
        {
            bool anyAnnotations = false;

            for (int i = 1; i <= doc.Pages.Count; i++) // pages are 1‑based
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still has {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
                Console.WriteLine("Validation succeeded: No annotations remain after flattening.");
            else
                Console.WriteLine("Validation failed: Some annotations were not removed.");
        }
    }
}