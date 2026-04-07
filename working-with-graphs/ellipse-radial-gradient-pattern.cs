using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "ellipse_pattern.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container to hold vector shapes
            // Width and height define the drawing area on the page
            Graph graph = new Graph(500, 500);
            page.Paragraphs.Add(graph);

            // Create an ellipse shape (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 200, 300, 150);

            // Define a radial gradient pattern as the fill brush
            // GradientRadialShading is a PatternColorSpace implementation
            GradientRadialShading radialPattern = new GradientRadialShading(
                Aspose.Pdf.Color.Yellow,   // start color
                Aspose.Pdf.Color.Blue);   // end color

            // Assign the pattern to the ellipse's fill via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = new Color { PatternColorSpace = radialPattern },
                Color = Aspose.Pdf.Color.Black,   // optional stroke color
                LineWidth = 1
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add a text fragment to verify that the pattern is rendered
            TextFragment tf = new TextFragment("Pattern Fill Verification");
            tf.Position = new Position(100, 100);
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(tf);

            // Save the document (PDF format inferred from extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with patterned ellipse saved to '{outputPath}'.");
    }
}