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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based indexed
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Current page dimensions
                    double currentWidth = page.PageInfo.Width;
                    double currentHeight = page.PageInfo.Height;

                    // Scale factor to achieve the target width while preserving aspect ratio
                    double scale = targetWidth / currentWidth;
                    double newHeight = currentHeight * scale;

                    // Resize the page
                    page.SetPageSize(targetWidth, newHeight);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"All pages resized to width {targetWidth} points and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}