using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hsl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example HSL values for a soft teal background
            // Hue: 0.5 (180°), Saturation: 0.3, Luminance: 0.9 (values are in 0‑1 range)
            Aspose.Pdf.Color hslColor = ColorFromHsl(0.5, 0.3, 0.9);

            // Apply the background color to each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Background belongs to the Page object, not PageInfo
                page.Background = hslColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HSL background to '{outputPath}'.");
    }

    /// <summary>
    /// Converts HSL (each component in the 0‑1 range) to an Aspose.Pdf.Color using RGB.
    /// </summary>
    private static Aspose.Pdf.Color ColorFromHsl(double h, double s, double l)
    {
        // Ensure hue wraps around
        h = h - Math.Floor(h);
        double r, g, b;

        if (Math.Abs(s) < 1e-10)
        {
            // Achromatic (grey)
            r = g = b = l;
        }
        else
        {
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;

            double HueToRgb(double t)
            {
                if (t < 0) t += 1;
                if (t > 1) t -= 1;
                if (t < 1.0 / 6) return p + (q - p) * 6 * t;
                if (t < 1.0 / 2) return q;
                if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
                return p;
            }

            r = HueToRgb(h + 1.0 / 3);
            g = HueToRgb(h);
            b = HueToRgb(h - 1.0 / 3);
        }

        // Aspose.Pdf.Color.FromRgb expects values in the 0‑1 range
        return Aspose.Pdf.Color.FromRgb(r, g, b);
    }
}
