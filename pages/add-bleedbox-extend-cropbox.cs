using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bleed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Use CropBox if defined; otherwise fall back to MediaBox
                Aspose.Pdf.Rectangle cropBox = page.CropBox ?? page.MediaBox;

                // Extend the box by 5 points on each side for bleed area
                Aspose.Pdf.Rectangle bleedBox = new Aspose.Pdf.Rectangle(
                    cropBox.LLX - 5,
                    cropBox.LLY - 5,
                    cropBox.URX + 5,
                    cropBox.URY + 5);

                page.BleedBox = bleedBox;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"BleedBox applied and saved to '{outputPath}'.");
    }
}