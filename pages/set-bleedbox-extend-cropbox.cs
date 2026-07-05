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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Use CropBox if defined, otherwise fall back to MediaBox
                Aspose.Pdf.Rectangle crop = page.CropBox ?? page.MediaBox;

                // Extend each side by 5 points to create the bleed area
                double left   = crop.LLX - 5;
                double bottom = crop.LLY - 5;
                double right  = crop.URX + 5;
                double top    = crop.URY + 5;

                // Set the BleedBox
                page.BleedBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"BleedBox set and saved to '{outputPath}'.");
    }
}