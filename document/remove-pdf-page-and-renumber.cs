using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the page number to remove (1‑based)
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageToDelete = 3; // change as needed

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number against the document's page count
            if (pageToDelete < 1 || pageToDelete > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageToDelete}. Document contains {doc.Pages.Count} pages.");
                return;
            }

            // Delete the specified page. PageCollection uses 1‑based indexing.
            doc.Pages.Delete(pageToDelete);

            // Remaining pages are automatically renumbered by Aspose.Pdf.
            // Save the modified document to the output path.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToDelete} removed. Output saved to '{outputPath}'.");
    }
}