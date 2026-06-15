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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Flatten all annotations using the PdfAnnotationEditor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.FlatteningAnnotations(); // removes annotation objects from the PDF
        editor.Save(flattenedPath);
        editor.Close(); // release resources held by the facade

        // Verify that no annotation objects remain by scanning the PDF object tree
        using (Document doc = new Document(flattenedPath))
        {
            bool anyAnnotations = false;

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
            {
                Console.WriteLine("Validation passed: No annotation objects remain after flattening.");
            }
            else
            {
                Console.WriteLine("Validation failed: Some annotations were not flattened.");
            }
        }
    }
}