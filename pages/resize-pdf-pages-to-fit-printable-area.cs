using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Printable area in points (A4 size)
            const double targetWidth = 595.0;
            const double targetHeight = 842.0;

            // Iterate pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double origWidth = page.PageInfo.Width;
                double origHeight = page.PageInfo.Height;

                // Compute scaling factor to fit within target area (do not enlarge)
                double scaleX = targetWidth / origWidth;
                double scaleY = targetHeight / origHeight;
                double scale = Math.Min(Math.Min(scaleX, scaleY), 1.0);

                if (scale < 1.0) // Resize only when shrinking is needed
                {
                    double newWidth = origWidth * scale;
                    double newHeight = origHeight * scale;

                    // Resize scales the page content proportionally
                    page.Resize(new Aspose.Pdf.PageSize((float)newWidth, (float)newHeight));
                }
            }

            // Save the resized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}