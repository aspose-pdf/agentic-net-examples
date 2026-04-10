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

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Flatten all annotations using the PdfAnnotationEditor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF into the editor
            editor.FlatteningAnnotations();          // Flatten every annotation in the document
            editor.Save(outputPath);                  // Persist the flattened PDF
            editor.Close();                          // Release resources held by the editor
        }

        // Verify that no annotation objects remain by scanning the PDF object tree
        using (Document doc = new Document(outputPath))
        {
            bool anyAnnotations = false;

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // If any annotations are still present, report them
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
                }
            }

            // Output validation result
            if (!anyAnnotations)
            {
                Console.WriteLine("Validation passed: no annotations remain after flattening.");
            }
            else
            {
                Console.WriteLine("Validation failed: some annotations were not flattened.");
            }
        }
    }
}