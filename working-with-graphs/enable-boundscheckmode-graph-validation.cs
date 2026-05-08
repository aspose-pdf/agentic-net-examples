using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, BoundsCheckMode, GraphInfo, Color, Rectangle

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define graph size (width x height in points)
            double graphWidth = 400;
            double graphHeight = 200;

            // Create a Graph object with the desired dimensions
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page (optional)
                Left = 50,
                Top  = 600
            };

            // Enable bounds checking: throw if a shape does not fit inside the graph area
            graph.Shapes.UpdateBoundsCheckMode(
                BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                graphWidth,
                graphHeight);

            // Example shape that fits within the graph bounds
            var rectFit = new Aspose.Pdf.Drawing.Rectangle(
                (float)10,   // X
                (float)10,   // Y
                (float)100,  // Width
                (float)80);  // Height
            rectFit.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectFit);

            // Uncomment the following block to see the exception in action.
            // This shape exceeds the graph width and will cause a BoundsNotFitException.
            /*
            var rectOverflow = new Aspose.Pdf.Drawing.Rectangle(
                (float)350, (float)150, (float)100, (float)80);
            rectOverflow.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 2f
            };
            // This line will throw because the shape does not fit.
            graph.Shapes.Add(rectOverflow);
            */

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "BoundsCheckGraph.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus is required for PDF creation on this platform. Skipping doc.Save().");
            }
        }
    }
}
