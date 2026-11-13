using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Desired aspect ratio 4:3 (width / height)
            const double targetRatio = 4.0 / 3.0;

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // MediaBox defines the original page size
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                double originalWidth  = mediaBox.URX - mediaBox.LLX;
                double originalHeight = mediaBox.URY - mediaBox.LLY;
                double currentRatio   = originalWidth / originalHeight;

                double llx = mediaBox.LLX;
                double lly = mediaBox.LLY;
                double urx = mediaBox.URX;
                double ury = mediaBox.URY;

                if (Math.Abs(currentRatio - targetRatio) < 0.0001)
                {
                    // Already at target ratio – no cropping needed
                    continue;
                }

                if (currentRatio > targetRatio)
                {
                    // Page is too wide – reduce width
                    double newWidth = originalHeight * targetRatio;
                    double delta    = (originalWidth - newWidth) / 2.0;
                    llx += delta;
                    urx -= delta;
                }
                else // currentRatio < targetRatio
                {
                    // Page is too tall – reduce height
                    double newHeight = originalWidth / targetRatio;
                    double delta     = (originalHeight - newHeight) / 2.0;
                    lly += delta;
                    ury -= delta;
                }

                // Apply the calculated crop box
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}