using System;
using System.IO;
using Aspose.Pdf;               // Core API namespace
using Aspose.Pdf.Text;          // For Color utilities if needed (not required here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cmyk_background.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define CMYK values (range 0.0 – 1.0). Example: 30% cyan, 20% magenta, 0% yellow, 10% black
            double c = 0.30;
            double m = 0.20;
            double y = 0.00;
            double k = 0.10;

            // Create a PDF Color from CMYK components
            Color cmykColor = Color.FromCmyk(c, m, y, k);

            // Apply the background color to each page (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = cmykColor;   // Set the page background color
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background: {outputPath}");
    }
}