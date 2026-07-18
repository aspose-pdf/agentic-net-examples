using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_cmyk.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // CMYK components (range 0.0 – 1.0). Adjust as needed for print fidelity.
            double c = 0.30; // Cyan
            double m = 0.20; // Magenta
            double y = 0.00; // Yellow
            double k = 0.10; // Black

            // Create a PDF Color from CMYK values
            Aspose.Pdf.Color cmykColor = Aspose.Pdf.Color.FromCmyk(c, m, y, k);

            // Apply the background color to every page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = cmykColor;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}