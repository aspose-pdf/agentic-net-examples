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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original dimensions
                double originalWidth  = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Compute scaling factor to achieve the target width while preserving aspect ratio
                double scaleFactor = targetWidth / originalWidth;
                double newHeight   = originalHeight * scaleFactor;

                // Apply new dimensions
                page.PageInfo.Width  = targetWidth;
                page.PageInfo.Height = newHeight;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized to width {targetWidth} points. Output saved to '{outputPath}'.");
    }
}