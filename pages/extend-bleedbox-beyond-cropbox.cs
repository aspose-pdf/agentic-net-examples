using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bleed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the current CropBox
                Aspose.Pdf.Rectangle cropBox = page.CropBox;

                // Create a new BleedBox that extends 5 points beyond each side of the CropBox
                Aspose.Pdf.Rectangle bleedBox = new Aspose.Pdf.Rectangle(
                    cropBox.LLX - 5,   // left
                    cropBox.LLY - 5,   // bottom
                    cropBox.URX + 5,   // right
                    cropBox.URY + 5    // top
                );

                // Assign the BleedBox to the page
                page.BleedBox = bleedBox;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated BleedBox: {outputPath}");
    }
}