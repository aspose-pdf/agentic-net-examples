using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "presentation_slides.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: load within a using block)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing as per rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size from MediaBox
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.Width;
                double origHeight = mediaBox.Height;

                // Desired 4:3 aspect ratio
                const double targetRatio = 4.0 / 3.0;

                double targetWidth, targetHeight;

                // Determine whether to limit by width or height
                if (origWidth / origHeight > targetRatio)
                {
                    // Page is too wide – limit by height
                    targetHeight = origHeight;
                    targetWidth  = origHeight * targetRatio;
                }
                else
                {
                    // Page is too tall – limit by width
                    targetWidth  = origWidth;
                    targetHeight = origWidth / targetRatio;
                }

                // Center the crop box within the original page
                double llx = mediaBox.LLX + (origWidth  - targetWidth)  / 2.0;
                double lly = mediaBox.LLY + (origHeight - targetHeight) / 2.0;
                double urx = llx + targetWidth;
                double ury = lly + targetHeight;

                // Set the CropBox (using fully qualified Rectangle to avoid ambiguity)
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}