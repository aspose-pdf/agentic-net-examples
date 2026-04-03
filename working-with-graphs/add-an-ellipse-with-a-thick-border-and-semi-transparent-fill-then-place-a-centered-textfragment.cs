using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

#nullable enable

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Draw an ellipse with a thick border and semi‑transparent fill
            // -------------------------------------------------
            // Define ellipse geometry (left, bottom, width, height)
            double left   = 100;   // X‑coordinate of left side
            double bottom = 400;   // Y‑coordinate of bottom side
            double width  = 200;   // Horizontal size
            double height = 100;   // Vertical size

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set visual properties via GraphInfo (no direct FillColor/LineWidth properties)
            ellipse.GraphInfo = new GraphInfo
            {
                // Semi‑transparent fill (alpha = 128 out of 255 ≈ 0.5)
                FillColor = Color.FromArgb(128, 51, 153, 204), // light‑blue with 50 % opacity
                // Thick black border
                Color     = Color.Black,                     // stroke color
                LineWidth = 3                                 // border thickness
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // 2. Place a centered TextFragment inside the ellipse
            // -------------------------------------------------
            string text = "Sample Text";

            // Create a TextFragment with the desired string
            TextFragment tf = new TextFragment(text);

            // Position the text at the geometric centre of the ellipse
            tf.Position = new Position(left + width / 2, bottom + height / 2);

            // Configure text appearance
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.White;
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // Note: TextFragmentState does not expose a VerticalAlignment property; the Position already centers the text vertically.

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // -------------------------------------------------
            // Save the resulting PDF (guard against missing libgdiplus on non‑Windows platforms)
            // -------------------------------------------------
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved with graphical content.");
                }
            }
        }

        Console.WriteLine("Program finished.");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
