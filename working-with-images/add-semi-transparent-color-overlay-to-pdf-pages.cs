using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_overlay.pdf";
        const string themeConfigPath = "theme.cfg"; // format: R,G,B,Opacity (0-1)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(themeConfigPath))
        {
            Console.Error.WriteLine($"Theme config not found: {themeConfigPath}");
            return;
        }

        // Parse theme configuration
        // Expected line e.g. "0.2,0.5,0.8,0.3"
        string[] parts = File.ReadAllText(themeConfigPath).Trim().Split(',');
        if (parts.Length != 4 ||
            !double.TryParse(parts[0], out double r) ||
            !double.TryParse(parts[1], out double g) ||
            !double.TryParse(parts[2], out double b) ||
            !double.TryParse(parts[3], out double opacity))
        {
            Console.Error.WriteLine("Invalid theme configuration. Expected format: R,G,B,Opacity (values 0-1).");
            return;
        }

        // Clamp values to valid range
        r = Math.Max(0, Math.Min(1, r));
        g = Math.Max(0, Math.Min(1, g));
        b = Math.Max(0, Math.Min(1, b));
        opacity = Math.Max(0, Math.Min(1, opacity));

        // Aspose.Pdf.Color does not expose an alpha channel directly.
        // To simulate semi‑transparent overlay we create a rectangle shape
        // with the desired fill color and then set the page's opacity via a stamp.
        // The stamp approach allows us to specify Opacity (0‑1).

        using (Document doc = new Document(inputPdf))
        {
            // Create a transparent PNG in memory that will serve as the overlay.
            // The PNG is a single pixel of the desired color; the stamp will stretch it.
            // Note: System.Drawing is used only for image generation (Windows‑only).
            // If running on non‑Windows platforms, replace this block with a pre‑created PNG.
            byte[] pngBytes;
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1, 1))
            {
                var color = System.Drawing.Color.FromArgb(
                    (int)(opacity * 255), // alpha
                    (int)(r * 255),
                    (int)(g * 255),
                    (int)(b * 255));
                bmp.SetPixel(0, 0, color);
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    pngBytes = ms.ToArray();
                }
            }

            // Iterate over all pages and apply the overlay stamp
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the generated PNG
                using (MemoryStream pngStream = new MemoryStream(pngBytes))
                {
                    ImageStamp overlayStamp = new ImageStamp(pngStream);
                    overlayStamp.Background = false; // draw on top of page content
                    overlayStamp.Opacity = (float)opacity; // overall stamp opacity
                    overlayStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    overlayStamp.VerticalAlignment   = VerticalAlignment.Center;
                    overlayStamp.Width  = page.Rect.Width;
                    overlayStamp.Height = page.Rect.Height;

                    // Add the stamp to the page
                    page.AddStamp(overlayStamp);
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}
