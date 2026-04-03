using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a canvas) with desired dimensions.
            // Use the double‑based constructor as the float overload is obsolete.
            Graph graph = new Graph(500.0, 500.0); // width, height in points

            // Define circle parameters: center coordinates and radius (float values).
            float centerX = 250f; // X‑coordinate of the circle center
            float centerY = 250f; // Y‑coordinate of the circle center
            float radius  = 100f; // Radius of the circle

            // Instantiate the Circle shape using the float‑based constructor.
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual appearance via GraphInfo (fill color, stroke color, line width).
            // LineWidth expects a float, so use a float literal.
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue, // Fill the circle with light blue
                Color     = Color.DarkBlue,  // Outline color
                LineWidth = 2f                // Outline thickness (float)
            };

            // Add the circle to the Graph's shape collection
            graph.Shapes.Add(circle);

            // Add the Graph (containing the circle) to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document only on platforms where GDI+ (or libgdiplus) is available.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with filled circle saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}
