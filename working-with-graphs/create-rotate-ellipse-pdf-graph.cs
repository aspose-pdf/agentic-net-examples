using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_rotated.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            const double graphWidth = 300.0;   // width of the graph container
            const double graphHeight = 200.0;  // height of the graph container
            const double graphLeft = 100.0;    // left (X) position on the page
            const double graphBottom = 200.0; // desired bottom Y coordinate on the page

            // Create the graph with double parameters (float overload is obsolete)
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = graphLeft,
                // Graph.Top defines the Y coordinate of the top edge; add height to place the bottom at graphBottom
                Top = graphBottom + graphHeight
            };

            // Create an ellipse (centerX, centerY, radiusX, radiusY) using float literals
            Ellipse ellipse = new Ellipse(150.0f, 150.0f, 80.0f, 40.0f);
            // Styling is done through the shape's GraphInfo
            ellipse.GraphInfo.FillColor = Color.LightBlue;
            ellipse.GraphInfo.Color = Color.DarkBlue; // stroke colour
            ellipse.GraphInfo.LineWidth = 2.0f;
            // Rotate the ellipse 45 degrees (counter‑clockwise)
            ellipse.GraphInfo.RotationAngle = 45.0;

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with rotated ellipse to '{outputPath}'.");
    }
}
