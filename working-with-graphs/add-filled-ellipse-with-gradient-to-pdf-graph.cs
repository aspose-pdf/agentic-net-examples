using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) and position it on the page
            var graph = new Graph(500, 500)
            {
                Left = 50,   // distance from the left edge of the page
                Top = 250    // distance from the top edge of the page
            };

            // Define the ellipse bounds within the graph (left, bottom, width, height)
            float ellipseLeft   = 150f;
            float ellipseBottom = 150f;
            float ellipseWidth  = 300f;
            float ellipseHeight = 200f;

            // Create the ellipse shape
            var ellipse = new Ellipse(ellipseLeft, ellipseBottom, ellipseWidth, ellipseHeight);

            // Configure a vertical linear gradient fill (light blue to dark blue)
            // Gradient direction is defined by the Start and End points of GradientAxialShading.
            var startPoint = new Point(ellipseLeft + ellipseWidth / 2, ellipseBottom + ellipseHeight); // top centre
            var endPoint   = new Point(ellipseLeft + ellipseWidth / 2, ellipseBottom);                // bottom centre

            ellipse.GraphInfo.FillColor = new Color
            {
                PatternColorSpace = new GradientAxialShading(Color.LightBlue, Color.DarkBlue)
                {
                    Start = startPoint,
                    End   = endPoint
                }
            };

            // Optionally set a stroke colour for the ellipse outline
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1.5f;

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled ellipse saved to '{outputPath}'.");
    }
}
