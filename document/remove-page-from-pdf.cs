using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageToRemove = 3; // 1‑based page number to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the requested page exists
            if (pageToRemove < 1 || pageToRemove > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageToRemove}. Document contains {doc.Pages.Count} pages.");
                return;
            }

            // Delete the specified page; Aspose.Pdf automatically renumbers remaining pages
            doc.Pages.Delete(pageToRemove);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToRemove} removed. Saved to '{outputPath}'.");
    }
}