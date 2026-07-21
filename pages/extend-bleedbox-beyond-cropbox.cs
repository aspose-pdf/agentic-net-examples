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

        // Load the PDF document (using rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the current CropBox
                Aspose.Pdf.Rectangle crop = page.CropBox;

                // Extend 5 points on each side to create the BleedBox
                double bleedLeft   = crop.LLX - 5;
                double bleedBottom = crop.LLY - 5;
                double bleedRight  = crop.URX + 5;
                double bleedTop    = crop.URY + 5;

                // Assign the new BleedBox
                page.BleedBox = new Aspose.Pdf.Rectangle(bleedLeft, bleedBottom, bleedRight, bleedTop);
            }

            // Save the modified PDF (using rule for disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with BleedBox extended: {outputPath}");
    }
}