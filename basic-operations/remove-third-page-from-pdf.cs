using System;
using System.IO;
using Aspose.Pdf;               // Core API for PDF manipulation
using Aspose.Pdf.Facades;      // Not needed here but kept for completeness

class RemoveThirdPage
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_third_page.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than three pages; nothing to delete.");
                return;
            }

            // Delete the third page (page numbers are 1‑based)
            doc.Pages.Delete(3);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed. Result saved to '{outputPath}'.");
    }
}