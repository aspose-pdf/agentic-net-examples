using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bleed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Current CropBox
                Aspose.Pdf.Rectangle crop = page.CropBox;

                // BleedBox extends 5 points beyond each side of CropBox
                Aspose.Pdf.Rectangle bleed = new Aspose.Pdf.Rectangle(
                    crop.LLX - 5,
                    crop.LLY - 5,
                    crop.URX + 5,
                    crop.URY + 5);

                page.BleedBox = bleed;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"BleedBox set and saved to '{outputPath}'.");
    }
}