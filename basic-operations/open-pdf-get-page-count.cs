using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF into a memory stream to avoid internal path‑related issues
            byte[] pdfBytes = File.ReadAllBytes(inputPath);
            using (var stream = new MemoryStream(pdfBytes))
            using (Document doc = new Document(stream))
            {
                // Pages collection is 1‑based; Count gives the total number of pages
                int pageCount = doc.Pages.Count;
                Console.WriteLine($"Document contains {pageCount} page(s).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error opening PDF: {ex.Message}");
        }
    }
}
