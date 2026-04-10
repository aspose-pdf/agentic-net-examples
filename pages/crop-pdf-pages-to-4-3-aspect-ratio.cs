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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size from MediaBox
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.Width;
                double origHeight = mediaBox.Height;

                // Desired aspect ratio 4:3
                const double targetRatio = 4.0 / 3.0;
                double newWidth, newHeight;

                double origRatio = origWidth / origHeight;
                if (origRatio > targetRatio)
                {
                    // Page is wider than 4:3 – crop width
                    newHeight = origHeight;
                    newWidth  = origHeight * targetRatio;
                }
                else
                {
                    // Page is taller than 4:3 – crop height
                    newWidth  = origWidth;
                    newHeight = origWidth / targetRatio;
                }

                // Center the crop box within the original page
                double llx = mediaBox.LLX + (origWidth  - newWidth)  / 2.0;
                double lly = mediaBox.LLY + (origHeight - newHeight) / 2.0;
                double urx = llx + newWidth;
                double ury = lly + newHeight;

                // Apply the new crop box
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the cropped PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}