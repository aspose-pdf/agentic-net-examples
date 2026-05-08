using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Flatten all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(); // removes annotation objects from the PDF
            editor.Save(outputPath);
        }

        // Verify that no annotation objects remain by scanning each page's annotation collection
        using (Document doc = new Document(outputPath))
        {
            bool anyAnnotations = false;

            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
            {
                Console.WriteLine("Validation passed: no annotation objects remain after flattening.");
            }
            else
            {
                Console.WriteLine("Validation failed: some annotations were not removed.");
            }
        }
    }
}