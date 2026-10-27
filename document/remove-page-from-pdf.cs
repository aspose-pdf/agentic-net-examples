using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageToRemove = 3; // 1‑based page number to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number
            if (pageToRemove < 1 || pageToRemove > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageToRemove}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Remove the specified page; remaining pages are automatically renumbered
            doc.Pages.Delete(pageToRemove);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToRemove} removed. Result saved to '{outputPath}'.");
    }
}