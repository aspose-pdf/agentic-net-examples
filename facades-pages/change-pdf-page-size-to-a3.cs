using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_A3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // A3 size in points (1 point = 1/72 inch)
            double a3Width = PageSize.A3.Width;
            double a3Height = PageSize.A3.Height;

            // Resize every page to A3
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = a3Width;
                page.PageInfo.Height = a3Height;
                // Optional: set orientation flag based on dimensions
                page.PageInfo.IsLandscape = a3Width > a3Height;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF pages resized to A3 and saved as '{outputPath}'.");
    }
}
