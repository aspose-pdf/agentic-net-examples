using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_rotated.pdf";

        // Suppress known vulnerability warning from Microsoft.Bcl.Memory (NU1903)
        #pragma warning disable NU1903
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – this holds vector shapes
            // Use the double‑precision constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 300.0);

            // Create an ellipse: left, bottom, width, height
            Ellipse ellipse = new Ellipse(100.0, 100.0, 200.0, 100.0);

            // Build a rotation matrix for 45 degrees (π/4 radians)
            Matrix rotationMatrix = Matrix.Rotation(Math.PI / 4);

            // Apply rotation to the ellipse via its GraphInfo.
            // GraphInfo.RotationAngle expects degrees, so we set 45.
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f,
                RotationAngle = 45 // rotate 45 degrees
            };

            // (Optional) Demonstrate using the matrix to transform the bounding rectangle
            // Rectangle bounds = new Rectangle(ellipse.Left, ellipse.Bottom,
            //                                 ellipse.Left + ellipse.Width, ellipse.Bottom + ellipse.Height);
            // Rectangle rotatedBounds = rotationMatrix.Transform(bounds);
            // ellipse.Left = rotatedBounds.LLX;
            // ellipse.Bottom = rotatedBounds.LLY;
            // ellipse.Width = rotatedBounds.URX - rotatedBounds.LLX;
            // ellipse.Height = rotatedBounds.URY - rotatedBounds.LLY;

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the graph.");
                    // The document may still be saved; if not, you could choose to skip saving.
                }
            }
        }
        #pragma warning restore NU1903

        Console.WriteLine($"Saved PDF with rotated ellipse to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
