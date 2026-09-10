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
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox
                Aspose.Pdf.Rectangle bleedBox = page.BleedBox;

                // Example adjustment: increase each side by 5 points (printer spec)
                double margin = 5.0;
                Aspose.Pdf.Rectangle adjustedBleedBox = new Aspose.Pdf.Rectangle(
                    bleedBox.LLX - margin, // left
                    bleedBox.LLY - margin, // bottom
                    bleedBox.URX + margin, // right
                    bleedBox.URY + margin  // top
                );

                // Apply the adjusted BleedBox back to the page
                page.BleedBox = adjustedBleedBox;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}