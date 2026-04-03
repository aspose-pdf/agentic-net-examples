using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size: 400x300 points)
            Graph graph = new Graph(400, 300);
            page.Paragraphs.Add(graph);

            // Define original ellipse parameters
            double left = 100;   // X coordinate of left side
            double bottom = 100; // Y coordinate of bottom side
            double width = 200;  // Width of the ellipse
            double height = 100; // Height of the ellipse

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Create a rotation matrix for 45 degrees (π/4 radians)
            Aspose.Pdf.Matrix rotationMatrix = Aspose.Pdf.Matrix.Rotation(Math.PI / 4);

            // Transform the bounding rectangle of the ellipse using the matrix
            Aspose.Pdf.Rectangle originalRect = new Aspose.Pdf.Rectangle(left, bottom, left + width, bottom + height);
            Aspose.Pdf.Rectangle transformedRect = rotationMatrix.Transform(originalRect);

            // Update ellipse position and size based on the transformed rectangle
            ellipse.Left = transformedRect.LLX;
            ellipse.Bottom = transformedRect.LLY;
            ellipse.Width = transformedRect.URX - transformedRect.LLX;
            ellipse.Height = transformedRect.URY - transformedRect.LLY;

            // Optional: set visual style for the ellipse
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect nested DllNotFoundException
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