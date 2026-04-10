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
            // Margin to trim from each side (points)
            const double margin = 50.0;

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Current page size
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Define a new CropBox reduced by the margin
                Aspose.Pdf.Rectangle cropBox = new Aspose.Pdf.Rectangle(
                    mediaBox.LLX + margin,
                    mediaBox.LLY + margin,
                    mediaBox.URX - margin,
                    mediaBox.URY - margin);

                // Apply the CropBox
                page.CropBox = cropBox;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}