using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox; if not set, fall back to MediaBox
                Aspose.Pdf.Rectangle bleedBox = page.BleedBox ?? page.MediaBox;

                // Example printer specification: expand the bleed area by 5 points on each side
                double offset = 5.0;
                Aspose.Pdf.Rectangle adjustedBleed = new Aspose.Pdf.Rectangle(
                    bleedBox.LLX - offset,
                    bleedBox.LLY - offset,
                    bleedBox.URX + offset,
                    bleedBox.URY + offset);

                // Apply the adjusted BleedBox back to the page
                page.BleedBox = adjustedBleed;
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}