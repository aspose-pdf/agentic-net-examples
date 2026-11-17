using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1;      // 1‑based page index (Aspose.Pdf uses 1‑based indexing)
        const int annotationIndex = 0; // zero‑based index within the page's Annotations collection

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Ensure the requested annotation exists
            if (annotationIndex < 0 || annotationIndex >= page.Annotations.Count)
            {
                Console.Error.WriteLine($"Annotation index {annotationIndex} is out of range. Page has {page.Annotations.Count} annotations.");
                return;
            }

            // Retrieve the annotation
            Annotation annotation = page.Annotations[annotationIndex];

            // Get the bounding rectangle (use fully qualified type to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = annotation.Rect; // corrected property name

            // Calculate dimensions
            double width  = rect.URX - rect.LLX;
            double height = rect.URY - rect.LLY;

            // Log the rectangle coordinates and its size
            Console.WriteLine($"Annotation Rectangle:");
            Console.WriteLine($"  Lower‑Left X : {rect.LLX}");
            Console.WriteLine($"  Lower‑Left Y : {rect.LLY}");
            Console.WriteLine($"  Upper‑Right X: {rect.URX}");
            Console.WriteLine($"  Upper‑Right Y: {rect.URY}");
            Console.WriteLine($"  Width  : {width}");
            Console.WriteLine($"  Height : {height}");
        }
    }
}
