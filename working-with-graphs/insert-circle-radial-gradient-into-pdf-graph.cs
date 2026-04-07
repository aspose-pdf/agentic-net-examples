using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "CircleWithGradient.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph canvas (width: 400, height: 400)
            Graph graph = new Graph(400, 400);
            page.Paragraphs.Add(graph);

            // Define the circle (center at (200,200), radius 100)
            Circle circle = new Circle(200f, 200f, 100f);

            // Create a radial gradient shading from Red to Blue
            GradientRadialShading radialGradient = new GradientRadialShading(
                Aspose.Pdf.Color.Red,   // start color
                Aspose.Pdf.Color.Blue   // end color
            );

            // Assign the gradient to a Color object's PatternColorSpace
            Aspose.Pdf.Color gradientFill = new Aspose.Pdf.Color
            {
                PatternColorSpace = radialGradient
            };

            // Apply the gradient fill to the circle via GraphInfo
            circle.GraphInfo = new GraphInfo
            {
                FillColor = gradientFill,
                // Optional: set stroke color and line width
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}