using System;
using System.IO;
using Aspose.Pdf; // Provides Document, Page, Rectangle, etc.

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
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions (MediaBox defines the full page size)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.URX - mediaBox.LLX;
                double origHeight = mediaBox.URY - mediaBox.LLY;

                const double targetRatio = 4.0 / 3.0; // Desired 4:3 aspect ratio
                double newWidth  = origWidth;
                double newHeight = origHeight;
                double offsetX   = 0;
                double offsetY   = 0;

                double currentRatio = origWidth / origHeight;

                if (currentRatio > targetRatio)
                {
                    // Page is too wide – shrink width, keep height
                    newWidth = origHeight * targetRatio;
                    offsetX = (origWidth - newWidth) / 2;
                }
                else if (currentRatio < targetRatio)
                {
                    // Page is too tall – shrink height, keep width
                    newHeight = origWidth / targetRatio;
                    offsetY = (origHeight - newHeight) / 2;
                }

                // Compute the new crop box coordinates (centered)
                double llx = mediaBox.LLX + offsetX;
                double lly = mediaBox.LLY + offsetY;
                double urx = llx + newWidth;
                double ury = lly + newHeight;

                // Apply the crop box (fully qualified Rectangle to avoid ambiguity)
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}