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
        const int pageNumber = 1;          // 1‑based page index (Aspose.Pdf uses 1‑based indexing)
        const int annotationIndex = 0;     // zero‑based index within the page's annotation collection

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Access the annotation collection of the page
            AnnotationCollection annotations = page.Annotations;

            // Ensure the annotation index is within range
            if (annotationIndex < 0 || annotationIndex >= annotations.Count)
            {
                Console.Error.WriteLine($"Annotation index {annotationIndex} is out of range. Page contains {annotations.Count} annotations.");
                return;
            }

            // Delete the specific annotation by its index
            annotations.Delete(annotationIndex);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Deleted annotation at index {annotationIndex} on page {pageNumber}. Saved to '{outputPath}'.");
    }
}