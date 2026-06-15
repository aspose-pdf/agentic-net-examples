using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1;        // 1‑based page index
        const int annotationIndex = 1;   // 1‑based annotation index on the page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            if (annotationIndex > page.Annotations.Count)
            {
                Console.Error.WriteLine($"Annotation {annotationIndex} not found on page {pageNumber}.");
                return;
            }

            // Retrieve the specific annotation
            Annotation annotation = page.Annotations[annotationIndex];

            // Get the bounding rectangle, taking page rotation into account
            Aspose.Pdf.Rectangle rect = annotation.GetRectangle(true);

            // Log rectangle dimensions
            Console.WriteLine("Annotation rectangle (considering rotation):");
            Console.WriteLine($"LLX: {rect.LLX}, LLY: {rect.LLY}, URX: {rect.URX}, URY: {rect.URY}");
        }
    }
}