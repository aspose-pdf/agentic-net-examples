using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "uniform.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the maximum width and height among all pages
            double maxWidth = 0;
            double maxHeight = 0;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double w = page.PageInfo.Width;
                double h = page.PageInfo.Height;

                if (w > maxWidth) maxWidth = w;
                if (h > maxHeight) maxHeight = h;
            }

            // Resize each page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // SetPageSize changes the page dimensions
                page.SetPageSize(maxWidth, maxHeight);
            }

            // Save the uniform PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Uniform PDF saved to '{outputPath}'.");
    }
}