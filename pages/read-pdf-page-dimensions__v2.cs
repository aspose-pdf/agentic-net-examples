using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate from 1 to Count inclusive
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (crop box or media box)
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Calculate width and height (in points)
                double width  = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt");
            }
        }
    }
}