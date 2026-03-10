using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // On macOS libgdiplus is required for Graph rendering.
            // If it is not available we skip the Graph and just save the PDF.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without the ellipse.");
                doc.Save(outputPath);
                return;
            }

            // Create a Graph container (size can be arbitrary, here 500x500)
            Graph graph = new Graph(500.0, 500.0); // use double literals as required by the new constructor

            // Create an ellipse: left, bottom, width, height (float literals)
            Ellipse ellipse = new Ellipse(100.0F, 400.0F, 200.0F, 100.0F);

            // Set visual properties via GraphInfo (fill color, stroke color, line width)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,   // Fill the ellipse
                Color = Color.DarkBlue,        // Stroke color
                LineWidth = 2                  // Stroke thickness
            };

            // Add the ellipse shape to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph (containing the ellipse) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse PDF saved to '{outputPath}'.");
    }
}
