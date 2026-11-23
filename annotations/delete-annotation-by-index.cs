using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 2;          // target page (1‑based indexing)
        const int annotationIndex = 0;     // zero‑based index of the annotation to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (doc.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber]; // Pages are 1‑based

            // Verify the annotation index is valid
            if (page.Annotations.Count <= annotationIndex)
            {
                Console.Error.WriteLine($"Page {pageNumber} has only {page.Annotations.Count} annotations.");
                return;
            }

            // Delete the annotation at the specified index.
            // AnnotationCollection uses 1‑based indexing for Delete()
            page.Annotations.Delete(annotationIndex + 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation removed and PDF saved to '{outputPath}'.");
    }
}
