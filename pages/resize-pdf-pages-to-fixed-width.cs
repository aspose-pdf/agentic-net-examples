using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";
        const double targetWidth = 800.0; // points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions
                double originalWidth  = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Compute scaling factor to achieve the target width
                double scale = targetWidth / originalWidth;
                double newHeight = originalHeight * scale;

                // Resize the page while preserving aspect ratio
                page.SetPageSize(targetWidth, newHeight);
            }

            // Save the modified document (using the provided save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized to width {targetWidth} points and saved to '{outputPath}'.");
    }
}