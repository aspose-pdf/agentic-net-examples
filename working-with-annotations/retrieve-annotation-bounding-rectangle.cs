using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1;          // 1‑based page index
        const int annotationIndex = 0;     // zero‑based index within the page's Annotations collection

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Validate annotation index
            if (annotationIndex < 0 || annotationIndex >= page.Annotations.Count)
            {
                Console.Error.WriteLine("Annotation index out of range.");
                return;
            }

            // Retrieve the specific annotation
            Annotation annot = page.Annotations[annotationIndex];

            // Get its bounding rectangle using the correct property (Rect)
            Aspose.Pdf.Rectangle rect = annot.Rect;

            // Compute width and height
            double width  = rect.URX - rect.LLX;
            double height = rect.URY - rect.LLY;

            // Log rectangle coordinates and dimensions
            Console.WriteLine($"Annotation on page {pageNumber}:");
            Console.WriteLine($"  Lower‑Left X : {rect.LLX}");
            Console.WriteLine($"  Lower‑Left Y : {rect.LLY}");
            Console.WriteLine($"  Upper‑Right X: {rect.URX}");
            Console.WriteLine($"  Upper‑Right Y: {rect.URY}");
            Console.WriteLine($"  Width        : {width}");
            Console.WriteLine($"  Height       : {height}");
        }
    }
}
