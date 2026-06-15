using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Color

class Program
{
    static void Main()
    {
        const string outputPath = "colored_page.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Desired HSL values (Hue: 200°, Saturation: 0.5, Lightness: 0.8)
            double hue = 200.0;          // 0‑360
            double saturation = 0.5;     // 0‑1
            double lightness = 0.8;      // 0‑1

            // Convert HSL to RGB (values 0‑1) – Aspose.Pdf.Color does not have a FromHsl method
            (double r, double g, double b) = HslToRgb(hue, saturation, lightness);

            // Set the page background using the RGB color
            page.Background = Color.FromRgb(r, g, b);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HSL‑derived background color to '{outputPath}'.");
    }

    // Helper: converts HSL to RGB (each component in the range 0‑1)
    private static (double r, double g, double b) HslToRgb(double h, double s, double l)
    {
        // Normalize hue to [0,360)
        h = h % 360;
        if (h < 0) h += 360;

        double c = (1 - Math.Abs(2 * l - 1)) * s; // chroma
        double hPrime = h / 60.0;
        double x = c * (1 - Math.Abs(hPrime % 2 - 1));

        double r1 = 0, g1 = 0, b1 = 0;
        if (0 <= hPrime && hPrime < 1)      { r1 = c; g1 = x; b1 = 0; }
        else if (1 <= hPrime && hPrime < 2) { r1 = x; g1 = c; b1 = 0; }
        else if (2 <= hPrime && hPrime < 3) { r1 = 0; g1 = c; b1 = x; }
        else if (3 <= hPrime && hPrime < 4) { r1 = 0; g1 = x; b1 = c; }
        else if (4 <= hPrime && hPrime < 5) { r1 = x; g1 = 0; b1 = c; }
        else if (5 <= hPrime && hPrime < 6) { r1 = c; g1 = 0; b1 = x; }

        double m = l - c / 2;
        return (r1 + m, g1 + m, b1 + m);
    }
}