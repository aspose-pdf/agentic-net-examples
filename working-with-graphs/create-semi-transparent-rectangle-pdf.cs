using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class TransparencyExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Begin a transparency layer (handled by using an ARGB color)
            // ------------------------------------------------------------

            // Use the double‑based Graph constructor (the older float constructor is obsolete)
            Graph graph = new Graph(400.0, 300.0); // width, height in points

            // Create a rectangle shape for the drawing canvas – note the use of the
            // Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) which is the type
            // expected by Graph.Shapes.
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // X (left)
                50f,   // Y (bottom)
                150f,  // Width
                100f   // Height
            );

            // Set visual properties via GraphInfo. Transparency is achieved by using an
            // ARGB color where the alpha channel defines the opacity (0‑255).
            rect.GraphInfo = new GraphInfo
            {
                // 128 = 50 % opacity, 51,153,204 = a teal colour (R,G,B)
                FillColor = Color.FromArgb(128, 51, 153, 204),
                Color = Color.Black, // stroke colour
                LineWidth = 1f
                // No Opacity property – transparency is encoded in the FillColor's alpha channel.
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // End of transparency layer – the ARGB fill colour already defines the
            // semi‑transparent rendering.
            // ------------------------------------------------------------

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            const string outputPath = "TransparencyLayerExample.pdf";
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                }
            }
        }
    }

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