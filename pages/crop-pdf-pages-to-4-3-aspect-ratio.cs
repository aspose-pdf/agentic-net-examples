using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions from MediaBox
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.URX - mediaBox.LLX;
                double origHeight = mediaBox.URY - mediaBox.LLY;

                const double targetRatio = 4.0 / 3.0;               // Desired 4:3 aspect ratio
                double currentRatio = origWidth / origHeight;

                double newWidth  = origWidth;
                double newHeight = origHeight;
                double left   = mediaBox.LLX;
                double bottom = mediaBox.LLY;

                if (currentRatio > targetRatio)
                {
                    // Page is too wide – shrink width, keep full height
                    newWidth = origHeight * targetRatio;
                    left = mediaBox.LLX + (origWidth - newWidth) / 2.0;
                }
                else if (currentRatio < targetRatio)
                {
                    // Page is too tall – shrink height, keep full width
                    newHeight = origWidth / targetRatio;
                    bottom = mediaBox.LLY + (origHeight - newHeight) / 2.0;
                }

                // Create a new crop rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle cropRect = new Aspose.Pdf.Rectangle(
                    left,
                    bottom,
                    left + newWidth,
                    bottom + newHeight);

                // Apply the crop box to the page
                page.CropBox = cropRect;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}