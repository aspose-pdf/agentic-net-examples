using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_graph.pdf";
        const double rotationDegrees = 45; // rotation angle in degrees

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph with specified width and height (use double literals)
            Graph graph = new Graph(400.0, 200.0);

            // Apply rotation to the graph (degrees)
            graph.GraphInfo.RotationAngle = rotationDegrees;

            // Add a rectangle shape to visualize the rotation
            // Rectangle constructor expects float values, so cast or use float literals (F suffix)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,          // lower‑left X
                0f,          // lower‑left Y
                200f,        // width
                100f);       // height
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF, handling platforms without GDI+
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
                    Console.WriteLine("GDI+ library not found; PDF saved may be incomplete.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect nested DllNotFoundException
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
