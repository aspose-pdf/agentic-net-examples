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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} page(s).");

            // Pages are 1‑based indexed
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Calculate width and height from rectangle coordinates
                double width = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt");
            }
        }
    }
}