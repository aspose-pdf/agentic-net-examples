using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path – can be passed as a command‑line argument or defaults to "sample.pdf"
        string inputPath = args.Length > 0 ? args[0] : "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} pages.");

            // Pages are 1‑based; iterate from 1 to Count inclusive
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                // The Rect property returns the page rectangle (crop box or media box)
                var rect = page.Rect;
                double width = rect.Width;
                double height = rect.Height;

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}