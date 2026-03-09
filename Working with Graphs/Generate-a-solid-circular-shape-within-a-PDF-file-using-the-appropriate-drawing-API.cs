using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // macOS requires libgdiplus for Graph rendering. If it is missing, skip the Graph.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("Running on macOS – Graph rendering requires libgdiplus. Saving PDF without the circle.");
                doc.Save(outputPath);
                return;
            }

            // Graph constructor now expects double values (width, height)
            Graph graph = new Graph(200.0, 200.0);

            // Circle constructor uses float values (centerX, centerY, radius)
            Circle circle = new Circle(100.0F, 100.0F, 80.0F);

            // Define visual appearance: fill color, border color, and line width
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.Blue,   // solid fill
                Color = Color.Black,      // border color
                LineWidth = 2
            };

            // Add the circle shape to the graph
            graph.Shapes.Add(circle);

            // Add the graph (containing the circle) to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with solid circle saved to '{outputPath}'.");
    }
}
