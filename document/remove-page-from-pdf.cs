using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const int pageToDelete  = 3;             // page number to remove (1‑based)

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Validate the requested page number
                if (pageToDelete < 1 || pageToDelete > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageToDelete}. Document contains {doc.Pages.Count} pages.");
                    return;
                }

                // Delete the specified page; Aspose.Pdf automatically renumbers the remaining pages
                doc.Pages.Delete(pageToDelete);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page {pageToDelete} removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}