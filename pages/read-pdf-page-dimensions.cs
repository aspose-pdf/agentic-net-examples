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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed (see page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // The Rect property returns the page rectangle (media box or crop box)
                // Width = URX - LLX, Height = URY - LLY
                double width  = page.Rect.URX - page.Rect.LLX;
                double height = page.Rect.URY - page.Rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}