using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define HSL values (Hue: 0‑360, Saturation and Lightness: 0‑1)
        double hue = 210;          // Example: a blue hue
        double saturation = 0.75;  // 75% saturation
        double lightness = 0.5;    // 50% lightness

        // Convert HSL to RGB (components in 0‑1 range)
        (double r, double g, double b) = HslToRgb(hue, saturation, lightness);

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Set the page background color using the RGB values
            page.Background = Color.FromRgb(r, g, b);

            // Save the result
            doc.Save("page_with_hsl_background.pdf");
        }
    }

    // Converts HSL to RGB where each component is in the range 0..1
    static (double r, double g, double b) HslToRgb(double h, double s, double l)
    {
        h = h % 360;
        double c = (1 - Math.Abs(2 * l - 1)) * s;
        double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
        double m = l - c / 2;

        double r1 = 0, g1 = 0, b1 = 0;
        if (h < 60) { r1 = c; g1 = x; b1 = 0; }
        else if (h < 120) { r1 = x; g1 = c; b1 = 0; }
        else if (h < 180) { r1 = 0; g1 = c; b1 = x; }
        else if (h < 240) { r1 = 0; g1 = x; b1 = c; }
        else if (h < 300) { r1 = x; g1 = 0; b1 = c; }
        else { r1 = c; g1 = 0; b1 = x; }

        return (r1 + m, g1 + m, b1 + m);
    }
}