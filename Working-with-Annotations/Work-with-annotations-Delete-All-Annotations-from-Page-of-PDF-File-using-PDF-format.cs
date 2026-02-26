using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // PDF after annotations removed
        const int    pageNumber = 1;                // page from which to delete all annotations (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (global rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Access the page (1‑based indexing) and delete all its annotations
            Page page = doc.Pages[pageNumber];
            page.Annotations.Delete();   // AnnotationCollection.Delete() removes every annotation on the page

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All annotations removed from page {pageNumber}. Saved to '{outputPath}'.");
    }
}