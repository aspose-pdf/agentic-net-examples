using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Path to the source PDF
        const string outputPath = "output.pdf";  // Path for the resulting PDF
        const int    pageToDelete = 3;           // Page number to remove (1‑based)

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageToDelete < 1 || pageToDelete > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageToDelete}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Delete the specified page; Aspose.Pdf automatically renumbers remaining pages
            doc.Pages.Delete(pageToDelete);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToDelete} removed. Result saved to '{outputPath}'.");
    }
}