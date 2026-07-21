using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            doc.Pages.Add();

            // Define HSL values (Hue: 0‑360, Saturation and Lightness: 0‑1)
            double hue = 210.0;        // Example: blue hue
            double saturation = 0.75;  // 75% saturation
            double lightness = 0.60;   // 60% lightness

            // Convert HSL to Aspose.Pdf.Color (RGB conversion)
            Aspose.Pdf.Color pageBg = ColorFromHsl(hue, saturation, lightness);

            // Set the background color of the first page
            doc.Pages[1].Background = pageBg;

            // Save the PDF
            string outputPath = "PageWithHslBackground.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}' with HSL background color.");
        }
    }

    // Helper method to convert HSL to Aspose.Pdf.Color (RGB based)
    private static Aspose.Pdf.Color ColorFromHsl(double h, double s, double l)
    {
        // Normalize hue to [0,1]
        h = (h % 360 + 360) % 360; // ensure positive
        h /= 360.0;

        double r, g, b;

        if (Math.Abs(s) < 0.000001)
        {
            // Achromatic (grey)
            r = g = b = l;
        }
        else
        {
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;
            r = HueToRgb(p, q, h + 1.0 / 3.0);
            g = HueToRgb(p, q, h);
            b = HueToRgb(p, q, h - 1.0 / 3.0);
        }

        // r, g, b are already in the 0‑1 range, pass them directly to Aspose.Pdf.Color
        return Aspose.Pdf.Color.FromRgb(r, g, b);
    }

    private static double HueToRgb(double p, double q, double t)
    {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1.0 / 6.0) return p + (q - p) * 6 * t;
        if (t < 1.0 / 2.0) return q;
        if (t < 2.0 / 3.0) return p + (q - p) * (2.0 / 3.0 - t) * 6;
        return p;
    }
}
