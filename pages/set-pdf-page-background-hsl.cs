using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define HSL values (Hue: 210°, Saturation: 0.5, Lightness: 0.8)
            double hue = 210.0;          // 0‑360 degrees
            double saturation = 0.5;    // 0‑1
            double lightness = 0.8;     // 0‑1

            // Convert HSL to RGB because Aspose.Pdf.Color does not provide FromHsl
            var (r, g, b) = HslToRgb(hue, saturation, lightness);
            // FromRgb expects values in the 0‑1 range
            Aspose.Pdf.Color bgColor = Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);

            // Apply the derived color as the page background
            page.Background = bgColor;

            // Save the PDF
            string outputPath = "PageWithHslBackground.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }

    // Helper: converts HSL (h in 0‑360, s and l in 0‑1) to RGB components (0‑255)
    private static (int r, int g, int b) HslToRgb(double h, double s, double l)
    {
        h = h % 360;
        double c = (1 - Math.Abs(2 * l - 1)) * s;
        double hPrime = h / 60.0;
        double x = c * (1 - Math.Abs(hPrime % 2 - 1));
        double r1 = 0, g1 = 0, b1 = 0;

        if (0 <= hPrime && hPrime < 1) { r1 = c; g1 = x; b1 = 0; }
        else if (1 <= hPrime && hPrime < 2) { r1 = x; g1 = c; b1 = 0; }
        else if (2 <= hPrime && hPrime < 3) { r1 = 0; g1 = c; b1 = x; }
        else if (3 <= hPrime && hPrime < 4) { r1 = 0; g1 = x; b1 = c; }
        else if (4 <= hPrime && hPrime < 5) { r1 = x; g1 = 0; b1 = c; }
        else if (5 <= hPrime && hPrime < 6) { r1 = c; g1 = 0; b1 = x; }

        double m = l - c / 2;
        int r = (int)Math.Round((r1 + m) * 255);
        int g = (int)Math.Round((g1 + m) * 255);
        int b = (int)Math.Round((b1 + m) * 255);
        return (r, g, b);
    }
}