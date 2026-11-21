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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Flatten annotations using PdfAnnotationEditor (the correct API).
        try
        {
            // Load the source PDF with Document.
            Document srcDoc = new Document(inputPath);

            // Bind the document to PdfAnnotationEditor and flatten.
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(srcDoc);
                annotEditor.FlatteningAnnotations(); // correct method name
                annotEditor.Save(flattenedPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during flattening: {ex.Message}");
            return;
        }

        // Load the flattened PDF and inspect its structure for any remaining annotations.
        try
        {
            using (Document doc = new Document(flattenedPath))
            {
                bool hasAnnotations = false;

                // Aspose.Pdf uses 1‑based page indexing.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    if (page.Annotations != null && page.Annotations.Count > 0)
                    {
                        hasAnnotations = true;
                        break;
                    }
                }

                Console.WriteLine(hasAnnotations
                    ? "Annotations still present after flattening."
                    : "No annotation objects found after flattening.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}
