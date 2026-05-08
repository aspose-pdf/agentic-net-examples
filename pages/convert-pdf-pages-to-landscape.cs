using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve current MediaBox dimensions
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double width = mediaBox.Width;
                double height = mediaBox.Height;

                // Swap width and height to convert to landscape orientation
                page.SetPageSize(height, width);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to landscape and saved to '{outputPath}'.");
    }
}