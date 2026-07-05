using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) and position it on the page
            // NOTE: Graph constructor expects double values (use literals with a decimal point)
            Graph graph = new Graph(400.0, 300.0)
            {
                Left = 100,
                Top  = 500
            };

            // Create an ellipse (left, bottom, width, height) within the graph
            Ellipse ellipse = new Ellipse(50, 50, 200, 150);

            // Configure a solid fill (gradient fill is not available in the current Aspose.Pdf version)
            // If a newer version that supports gradient fill is used, the following properties can be added:
            //   FillColor2, FillMode = FillMode.Gradient, GradientAngle = <float>
            ellipse.GraphInfo = new GraphInfo
            {
                // Outline
                Color     = Color.Black,
                LineWidth = 1f,
                // Solid fill (blue)
                FillColor = Color.Blue
            };

            // Add the ellipse to the graph's shape collection
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
