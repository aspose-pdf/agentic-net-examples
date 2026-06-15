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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size from MediaBox (always defined)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.Width;
                double origHeight = mediaBox.Height;

                // Desired aspect ratio 4:3 (width : height)
                const double targetRatio = 4.0 / 3.0;
                double origRatio = origWidth / origHeight;

                double newWidth, newHeight;

                if (origRatio > targetRatio)
                {
                    // Page is too wide – keep height, reduce width
                    newHeight = origHeight;
                    newWidth  = origHeight * targetRatio;
                }
                else
                {
                    // Page is too tall – keep width, reduce height
                    newWidth  = origWidth;
                    newHeight = origWidth / targetRatio;
                }

                // Center the crop box
                double llx = mediaBox.LLX + (origWidth  - newWidth)  / 2.0;
                double lly = mediaBox.LLY + (origHeight - newHeight) / 2.0;
                double urx = llx + newWidth;
                double ury = lly + newHeight;

                // Set the CropBox (lifecycle rule: use property setter)
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}