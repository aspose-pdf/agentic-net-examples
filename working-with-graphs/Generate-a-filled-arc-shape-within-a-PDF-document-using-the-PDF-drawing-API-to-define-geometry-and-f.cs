using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "filled_arc.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // On macOS the Graph class needs libgdiplus. If it is missing, skip drawing the graph.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without the filled arc.");
                doc.Save(outputPath);
                return;
            }

            // Create a Graph container (use double literals as the constructor now expects double)
            Graph graph = new Graph(500.0, 500.0);

            // Define an arc (Arc constructor still expects float values)
            Arc arc = new Arc(250f, 250f, 100f, 0f, 180f)
            {
                GraphInfo = new GraphInfo
                {
                    // LightBlue = RGB(173,216,230)
                    FillColor = Color.FromRgb(173, 216, 230),
                    // No outline – either omit the Color property or set a transparent-like color.
                    // Setting LineWidth to 0 effectively hides the outline.
                    LineWidth = 0
                }
            };

            // Add the arc shape to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled arc saved to '{outputPath}'.");
    }
}
