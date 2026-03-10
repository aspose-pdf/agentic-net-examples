using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_page.pdf";
        const int pageNumber = 3; // 1‑based index of the page to extract

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {sourceDoc.Pages.Count} pages.");
                return;
            }

            // Create a new empty PDF document
            using (Document singlePageDoc = new Document())
            {
                // Add the selected page to the new document
                singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                // Save the new PDF containing only the extracted page
                singlePageDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Page {pageNumber} saved to '{outputPath}'.");
    }
}