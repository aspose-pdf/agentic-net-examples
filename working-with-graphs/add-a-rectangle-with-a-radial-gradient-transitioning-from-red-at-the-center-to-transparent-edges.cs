using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "radial_gradient_rectangle.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangle position and size (left, bottom, width, height) – use float literals as required by Aspose.Pdf.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                100f,   // Left
                400f,   // Bottom
                300f,   // Width
                200f);  // Height

            // Create a radial gradient shading: red at the center, transparent at the edges
            GradientRadialShading radialShading = new GradientRadialShading(
                Aspose.Pdf.Color.Red,               // start (center) color
                Aspose.Pdf.Color.Transparent);     // end (edge) color

            // Center point of the rectangle
            double centerX = rect.Left + rect.Width / 2;
            double centerY = rect.Bottom + rect.Height / 2;

            // Configure gradient geometry
            radialShading.Start = new Aspose.Pdf.Point(centerX, centerY);
            radialShading.End = new Aspose.Pdf.Point(centerX, centerY);
            radialShading.StartingRadius = 0; // radius at center
            radialShading.EndingRadius = Math.Min(rect.Width, rect.Height) / 2; // radius at edges

            // Assign the gradient to a Color object via PatternColorSpace
            Aspose.Pdf.Color fillColor = new Aspose.Pdf.Color();
            fillColor.PatternColorSpace = radialShading;

            // Set visual properties of the rectangle (fill with gradient, black border)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float as required by GraphInfo
            };

            // Add the rectangle to a Graph container and place it on the page
            // Use the double‑based Graph constructor (deprecated float overload avoided)
            Graph graph = new Graph(500.0, 500.0); // width, height as double
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with radial gradient rectangle saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved on non‑Windows platform to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf.Drawing.Graph.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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
