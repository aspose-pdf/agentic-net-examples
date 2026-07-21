using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (crop box or media box)
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Calculate width and height from rectangle coordinates
                double width  = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                // Log the dimensions
                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}