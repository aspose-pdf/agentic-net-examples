using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // path to the PDF file
        const int pageNumber = 1;                      // 1‑based page index
        const int annotationIndex = 1;                 // 1‑based annotation index on the page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageNumber}");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Validate annotation index
            if (annotationIndex < 1 || annotationIndex > page.Annotations.Count)
            {
                Console.Error.WriteLine($"Annotation index {annotationIndex} out of range (count={page.Annotations.Count})");
                return;
            }

            // Retrieve the specific annotation
            Annotation annotation = page.Annotations[annotationIndex];

            // Get the bounding rectangle, taking page rotation into account
            Aspose.Pdf.Rectangle rect = annotation.GetRectangle(true);

            // Log rectangle dimensions
            Console.WriteLine("Annotation Bounding Rectangle:");
            Console.WriteLine($"  Lower‑Left X : {rect.LLX}");
            Console.WriteLine($"  Lower‑Left Y : {rect.LLY}");
            Console.WriteLine($"  Upper‑Right X: {rect.URX}");
            Console.WriteLine($"  Upper‑Right Y: {rect.URY}");
        }
    }
}