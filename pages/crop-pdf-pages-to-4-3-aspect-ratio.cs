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
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions (points)
                double origWidth  = page.PageInfo.Width;
                double origHeight = page.PageInfo.Height;

                // Desired aspect ratio 4:3
                const double targetRatio = 4.0 / 3.0;
                double newWidth, newHeight;

                double origRatio = origWidth / origHeight;

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
                double llx = (origWidth  - newWidth)  / 2.0;
                double lly = (origHeight - newHeight) / 2.0;
                double urx = llx + newWidth;
                double ury = lly + newHeight;

                // Apply the crop box (use fully qualified Rectangle to avoid ambiguity)
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}