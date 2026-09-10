using System;
using Aspose.Pdf;

class Program
{
    // Convert HSL (hue 0‑360, saturation 0‑1, luminance 0‑1) to an Aspose.Pdf.Color
    static Color ColorFromHsl(double hue, double saturation, double luminance)
    {
        hue = hue % 360;
        double c = (1 - Math.Abs(2 * luminance - 1)) * saturation;
        double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
        double m = luminance - c / 2;

        double r1 = 0, g1 = 0, b1 = 0;
        if (hue < 60)      { r1 = c; g1 = x; b1 = 0; }
        else if (hue < 120){ r1 = x; g1 = c; b1 = 0; }
        else if (hue < 180){ r1 = 0; g1 = c; b1 = x; }
        else if (hue < 240){ r1 = 0; g1 = x; b1 = c; }
        else if (hue < 300){ r1 = x; g1 = 0; b1 = c; }
        else               { r1 = c; g1 = 0; b1 = x; }

        double r = r1 + m;
        double g = g1 + m;
        double b = b1 + m;

        // Aspose.Pdf.Color.FromRgb expects values in the range 0‑1
        return Color.FromRgb(r, g, b);
    }

    static void Main()
    {
        const string outputPath = "page_with_hsl_background.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Desired HSL values (example: hue=210°, saturation=0.6, luminance=0.5)
            double hue = 210.0;
            double saturation = 0.6;
            double luminance = 0.5;

            // Set the page background using the HSL‑derived color
            page.Background = ColorFromHsl(hue, saturation, luminance);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}