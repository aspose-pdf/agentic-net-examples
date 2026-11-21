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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdf = new Document(inputPath))
        {
            // Ensure the requested page exists (pages are 1‑based)
            if (pageToRemove < 1 || pageToRemove > pdf.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageToRemove} is out of range. Document has {pdf.Pages.Count} pages.");
                return;
            }

            // Delete the specified page; remaining pages are automatically renumbered
            pdf.Pages.Delete(pageToRemove);

            // Save the modified document (default format is PDF)
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToRemove} removed. Saved to '{outputPath}'.");
    }
}