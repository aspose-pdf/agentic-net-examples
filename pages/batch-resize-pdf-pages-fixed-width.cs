using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const double targetWidth = 800.0; // points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page collection)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions (points)
                double originalWidth  = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Compute new height to preserve aspect ratio
                double newHeight = originalHeight * (targetWidth / originalWidth);

                // Resize the page to the target width while keeping the aspect ratio
                page.SetPageSize(targetWidth, newHeight);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized to width {targetWidth} points and saved to '{outputPath}'.");
    }
}