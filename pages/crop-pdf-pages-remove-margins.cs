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

        // Margins to trim (points). Adjust as needed.
        const double leftMargin   = 36; // 0.5 inch
        const double rightMargin  = 36;
        const double topMargin    = 36;
        const double bottomMargin = 36;

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Pages collection uses 1‑based indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page size.
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Compute new CropBox coordinates by removing the margins.
                double llx = mediaBox.LLX + leftMargin;
                double lly = mediaBox.LLY + bottomMargin;
                double urx = mediaBox.URX - rightMargin;
                double ury = mediaBox.URY - topMargin;

                // Set the CropBox. Fully qualify Rectangle to avoid ambiguity.
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}