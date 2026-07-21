using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_4by3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions from MediaBox (full page size)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double origWidth  = mediaBox.Width;
                double origHeight = mediaBox.Height;

                const double targetRatio = 4.0 / 3.0;          // Desired 4:3 aspect ratio
                double origRatio = origWidth / origHeight;

                double newWidth, newHeight;
                double offsetX = 0, offsetY = 0;

                if (origRatio > targetRatio)
                {
                    // Page is wider than 4:3 – keep height, shrink width
                    newHeight = origHeight;
                    newWidth  = newHeight * targetRatio;
                    offsetX   = (origWidth - newWidth) / 2.0; // Center horizontally
                }
                else
                {
                    // Page is taller than 4:3 – keep width, shrink height
                    newWidth  = origWidth;
                    newHeight = newWidth / targetRatio;
                    offsetY   = (origHeight - newHeight) / 2.0; // Center vertically
                }

                // Compute lower‑left (llx,lly) and upper‑right (urx,ury) coordinates
                double llx = mediaBox.LLX + offsetX;
                double lly = mediaBox.LLY + offsetY;
                double urx = llx + newWidth;
                double ury = lly + newHeight;

                // Apply the new crop box to the page
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}