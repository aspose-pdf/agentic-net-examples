using System;
using Aspose.Pdf;

public class SetPageBackgroundHsl
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define HSL values (example: hue = 200°, saturation = 0.7, lightness = 0.5)
            double hue = 200.0;
            double saturation = 0.7;
            double lightness = 0.5;

            // Convert HSL to Aspose.Pdf.Color (values must be in the 0..1 range)
            Aspose.Pdf.Color backgroundColor = ColorFromHsl(hue, saturation, lightness);

            // Set the page background color
            page.Background = backgroundColor;

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }

    private static Aspose.Pdf.Color ColorFromHsl(double h, double s, double l)
    {
        // Convert HSL to RGB (0‑255 range)
        double c = (1.0 - Math.Abs(2.0 * l - 1.0)) * s;
        double hPrime = h / 60.0;
        double x = c * (1.0 - Math.Abs(hPrime % 2.0 - 1.0));

        double r1 = 0.0, g1 = 0.0, b1 = 0.0;

        if (0.0 <= hPrime && hPrime < 1.0)
        {
            r1 = c; g1 = x; b1 = 0.0;
        }
        else if (1.0 <= hPrime && hPrime < 2.0)
        {
            r1 = x; g1 = c; b1 = 0.0;
        }
        else if (2.0 <= hPrime && hPrime < 3.0)
        {
            r1 = 0.0; g1 = c; b1 = x;
        }
        else if (3.0 <= hPrime && hPrime < 4.0)
        {
            r1 = 0.0; g1 = x; b1 = c;
        }
        else if (4.0 <= hPrime && hPrime < 5.0)
        {
            r1 = x; g1 = 0.0; b1 = c;
        }
        else if (5.0 <= hPrime && hPrime < 6.0)
        {
            r1 = c; g1 = 0.0; b1 = x;
        }

        double m = l - c / 2.0;
        int r = (int)Math.Round((r1 + m) * 255.0);
        int g = (int)Math.Round((g1 + m) * 255.0);
        int b = (int)Math.Round((b1 + m) * 255.0);

        // Aspose.Pdf.Color expects components in the 0..1 range, so normalise the 0‑255 values.
        return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
    }
}
