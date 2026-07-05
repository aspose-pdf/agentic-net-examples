using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the amount to trim from each side (points; 1 inch = 72 points)
            const double marginLeft   = 36; // 0.5 inch
            const double marginRight  = 36;
            const double marginTop    = 36;
            const double marginBottom = 36;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the original media box (full page size)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Calculate new CropBox coordinates by subtracting the margins
                double llx = mediaBox.LLX + marginLeft;
                double lly = mediaBox.LLY + marginBottom;
                double urx = mediaBox.URX - marginRight;
                double ury = mediaBox.URY - marginTop;

                // Set the CropBox to the new rectangle
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}