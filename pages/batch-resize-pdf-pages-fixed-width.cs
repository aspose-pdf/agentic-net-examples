using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";
        const double targetWidth = 800.0; // points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double originalWidth = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Compute scaling factor to achieve the target width while preserving aspect ratio
                double scale = targetWidth / originalWidth;
                double newHeight = originalHeight * scale;

                // Apply new dimensions
                page.PageInfo.Width = targetWidth;
                page.PageInfo.Height = newHeight;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
    }
}