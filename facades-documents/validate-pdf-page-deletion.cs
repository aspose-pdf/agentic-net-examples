using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output path after deletion
        const string inputPath = "input.pdf";
        const string outputPath = "output_deleted.pdf";

        // Page number to delete (1‑based indexing)
        const int pageToDelete = 1;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Get the original page count
        // ------------------------------------------------------------
        int beforeCount;
        using (Document doc = new Document(inputPath))
        {
            beforeCount = doc.Pages.Count;
        }

        // ------------------------------------------------------------
        // Delete the specified page using Document API
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Delete the page (1‑based index)
            doc.Pages.Delete(pageToDelete);
            // Save the modified PDF
            doc.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Get the page count after deletion
        // ------------------------------------------------------------
        int afterCount;
        using (Document doc = new Document(outputPath))
        {
            afterCount = doc.Pages.Count;
        }

        // ------------------------------------------------------------
        // Validate that the page count decreased by one
        // ------------------------------------------------------------
        if (afterCount == beforeCount - 1)
        {
            Console.WriteLine($"Success: page count reduced from {beforeCount} to {afterCount}.");
        }
        else
        {
            Console.WriteLine($"Failure: expected {beforeCount - 1} pages after deletion, but got {afterCount}.");
        }
    }
}
