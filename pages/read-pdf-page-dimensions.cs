using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} pages.");

            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (media box) and compute width/height
                Aspose.Pdf.Rectangle rect = page.Rect;
                double width  = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}