using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container for drawing primitives
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Ellipse: left, bottom, width, height
            Ellipse ellipse = new Ellipse(100, 500, 300, 300);

            // Radial gradient fill (outer -> inner)
            GradientRadialShading radial = new GradientRadialShading(
                Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9),   // light outer color
                Aspose.Pdf.Color.FromRgb(0.4, 0.4, 0.8)    // darker inner color
            );
            ellipse.GraphInfo.FillColor = new Aspose.Pdf.Color { PatternColorSpace = radial };

            // Border (stroke) color and width
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.DarkGray;
            ellipse.GraphInfo.LineWidth = 2;

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
