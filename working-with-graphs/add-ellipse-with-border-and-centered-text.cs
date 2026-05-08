using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_with_text.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define ellipse position and size
            double left   = 100;   // X coordinate of left side
            double bottom = 400;   // Y coordinate of bottom side
            double width  = 300;   // Width of the ellipse
            double height = 200;   // Height of the ellipse

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            ellipse.GraphInfo = new GraphInfo
            {
                // Semi‑transparent fill (adjust opacity if supported)
                FillColor = Color.FromRgb(0.8, 0.9, 1.0),
                // If GraphInfo supports a Transparency property, uncomment the line below:
                // Transparency = 0.5,
                Color = Color.Blue,   // Border (stroke) color
                LineWidth = 5          // Thick border
            };

            // Add the ellipse to a Graph container and then to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Create a centered TextFragment
            string text = "Hello Ellipse";
            TextFragment tf = new TextFragment(text);
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.DarkBlue;

            // Position the text at the centre of the ellipse
            double centerX = left + width / 2;
            double centerY = bottom + height / 2;
            tf.Position = new Position(centerX, centerY);
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // Note: TextFragmentState does not provide a VerticalAlignment property.
            // The Y coordinate is set to the vertical centre of the ellipse.

            // Append the text to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
