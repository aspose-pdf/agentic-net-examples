using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (media box or crop box)
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Calculate width and height
                double width  = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt");
            }
        }
    }
}