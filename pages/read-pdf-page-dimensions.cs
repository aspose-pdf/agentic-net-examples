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
            // Pages are 1‑based; iterate from 1 to Count inclusive
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // The Rect property returns an Aspose.Pdf.Rectangle representing the page size
                Aspose.Pdf.Rectangle rect = page.Rect;

                double width  = rect.Width;   // page width
                double height = rect.Height;  // page height

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}