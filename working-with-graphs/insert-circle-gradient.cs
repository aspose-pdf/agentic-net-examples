using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace InsertCircleGradientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file.
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF and add a graph with a gradient‑filled circle.
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing).
                Page page = doc.Pages[1];

                // Create a graph canvas.
                Graph graph = new Graph(500f, 500f);
                page.Paragraphs.Add(graph);

                // Create a circle shape.
                Circle circle = new Circle(250f, 250f, 100f);

                // Create a radial gradient shading from red to blue.
                GradientRadialShading radialGradient = new GradientRadialShading(Aspose.Pdf.Color.Red, Aspose.Pdf.Color.Blue);

                // Assign the gradient to a Color object via its PatternColorSpace.
                Aspose.Pdf.Color gradientColor = new Aspose.Pdf.Color();
                gradientColor.PatternColorSpace = radialGradient;

                // Apply the gradient fill to the circle.
                circle.GraphInfo.FillColor = gradientColor;

                // Add the circle to the graph.
                graph.Shapes.Add(circle);

                // Save the modified document.
                doc.Save("output.pdf");
            }
        }
    }
}
