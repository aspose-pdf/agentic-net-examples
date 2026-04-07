using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a canvas for vector shapes)
            Graph graph = new Graph(400, 300);
            page.Paragraphs.Add(graph);

            // Create an ellipse shape (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 100, 200, 150);

            // Configure the ellipse border (stroke) to dark gray
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.DarkGray, // border color
                LineWidth = 2                       // optional border thickness
            };

            // Create a radial gradient shading (from light gray to white)
            GradientRadialShading radial = new GradientRadialShading(
                Aspose.Pdf.Color.LightGray,
                Aspose.Pdf.Color.White);

            // Define gradient geometry (center point and radii)
            radial.Start = new Point(200, 175); // center of the ellipse
            radial.End = new Point(200, 175);
            radial.StartingRadius = 0;
            radial.EndingRadius = 100;

            // Apply the gradient as the fill color of the ellipse
            Aspose.Pdf.Color fillColor = new Aspose.Pdf.Color
            {
                PatternColorSpace = radial
            };
            ellipse.GraphInfo.FillColor = fillColor;

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}