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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original dimensions
                double originalWidth  = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Compute scaling factor to achieve the target width
                double scale = targetWidth / originalWidth;

                // New height preserving aspect ratio
                double newHeight = originalHeight * scale;

                // Apply the new size
                page.SetPageSize(targetWidth, newHeight);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized to width {targetWidth} points. Saved as '{outputPath}'.");
    }
}