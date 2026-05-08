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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the maximum width and height among all pages
            double maxWidth = 0;
            double maxHeight = 0;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double width = page.Rect.Width;
                double height = page.Rect.Height;

                if (width > maxWidth) maxWidth = width;
                if (height > maxHeight) maxHeight = height;
            }

            // Resize every page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Resize(new PageSize((float)maxWidth, (float)maxHeight));
                // Alternative: page.SetPageSize(maxWidth, maxHeight);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Uniform PDF saved to '{outputPath}'.");
    }
}