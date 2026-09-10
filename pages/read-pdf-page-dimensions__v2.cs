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

        // Open the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} page(s).");

            // Pages are 1‑based indexed
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                // Fully qualified Rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = page.Rect;

                double width = rect.Width;
                double height = rect.Height;

                Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
            }
        }
    }
}