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
        const int annotationIndex = 1;   // 1‑based annotation index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page existence
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Validate annotation existence on the page
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
            Console.WriteLine("Annotation Rectangle (considering rotation):");
            Console.WriteLine($"LLX: {rect.LLX}, LLY: {rect.LLY}, URX: {rect.URX}, URY: {rect.URY}");
            Console.WriteLine($"Width: {rect.URX - rect.LLX}, Height: {rect.URY - rect.LLY}");
        }
    }
}