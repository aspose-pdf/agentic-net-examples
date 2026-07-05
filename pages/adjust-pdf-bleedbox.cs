using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Annotations;        // For printer mark annotations if needed

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox (use fully qualified type to avoid ambiguity)
                Aspose.Pdf.Rectangle bleedBox = page.BleedBox;

                // Example adjustment: expand the bleed area by 5 points on each side
                // Ensure we don't create negative coordinates
                double left   = Math.Max(bleedBox.LLX - 5, 0);
                double bottom = Math.Max(bleedBox.LLY - 5, 0);
                double right  = bleedBox.URX + 5;
                double top    = bleedBox.URY + 5;

                // Assign the adjusted BleedBox back to the page
                page.BleedBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}