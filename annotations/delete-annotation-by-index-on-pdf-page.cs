using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            const int pageNumber = 4;
            const int annotationIndex = 2; // zero‑based index within the Annotations collection

            if (doc.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; cannot access page {pageNumber}.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Ensure the requested annotation exists
            if (page.Annotations.Count > annotationIndex)
            {
                // Remove the annotation at the specified index (AnnotationCollection uses 1‑based indexing for Delete)
                page.Annotations.Delete(annotationIndex + 1);
                Console.WriteLine($"Removed annotation at index {annotationIndex} on page {pageNumber}.");
            }
            else
            {
                Console.Error.WriteLine($"Page {pageNumber} has only {page.Annotations.Count} annotations; cannot remove index {annotationIndex}.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
