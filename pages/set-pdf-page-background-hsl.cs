using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Convert HSL (h:0‑360, s:0‑1, l:0‑1) to an Aspose.Pdf.Color instance.
    static Color ColorFromHsl(double h, double s, double l)
    {
        // Compute chroma.
        double c = (1 - Math.Abs(2 * l - 1)) * s;
        // Hue sector.
        double hPrime = h / 60.0;
        double x = c * (1 - Math.Abs(hPrime % 2 - 1));

        double r1 = 0, g1 = 0, b1 = 0;
        if (0 <= hPrime && hPrime < 1)      { r1 = c; g1 = x; }
        else if (1 <= hPrime && hPrime < 2) { r1 = x; g1 = c; }
        else if (2 <= hPrime && hPrime < 3) { g1 = c; b1 = x; }
        else if (3 <= hPrime && hPrime < 4) { g1 = x; b1 = c; }
        else if (4 <= hPrime && hPrime < 5) { r1 = x; b1 = c; }
        else if (5 <= hPrime && hPrime < 6) { r1 = c; b1 = x; }

        // Add match lightness.
        double m = l - c / 2;
        int r = (int)Math.Round((r1 + m) * 255);
        int g = (int)Math.Round((g1 + m) * 255);
        int b = (int)Math.Round((b1 + m) * 255);

        // Create Aspose.Pdf.Color from RGB components.
        return Color.FromRgb(r, g, b);
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hsl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Desired HSL values (example: hue=210°, saturation=0.4, luminance=0.9).
            double hue        = 210.0; // 0‑360
            double saturation = 0.4;   // 0‑1
            double luminance  = 0.9;   // 0‑1

            // Create the background color from HSL.
            Color hslBackground = ColorFromHsl(hue, saturation, luminance);

            // Apply the background color to each page.
            foreach (Page page in doc.Pages)
            {
                page.Background = hslBackground;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HSL background to '{outputPath}'.");
    }
}