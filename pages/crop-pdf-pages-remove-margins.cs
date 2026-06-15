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
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the original page size (MediaBox)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Define margins to trim (example: 20 units from each side)
                double leftMargin   = 20;
                double bottomMargin = 20;
                double rightMargin  = 20;
                double topMargin    = 20;

                // Compute new CropBox coordinates
                double llx = mediaBox.LLX + leftMargin;
                double lly = mediaBox.LLY + bottomMargin;
                double urx = mediaBox.URX - rightMargin;
                double ury = mediaBox.URY - topMargin;

                // Set the CropBox to the new rectangle (fully qualified type to avoid ambiguity)
                page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }

            // Save the modified PDF (saving without explicit SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}