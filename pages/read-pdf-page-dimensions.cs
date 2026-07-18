using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} pages.");

            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                // The Rect property gives the page rectangle (media box or crop box)
                var rect = page.Rect;
                double width = rect.Width;
                double height = rect.Height;

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}