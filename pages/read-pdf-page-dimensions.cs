using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Rectangle representing the page size (crop box or media box)
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Compute width and height
                double width  = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt");
            }
        }
    }
}