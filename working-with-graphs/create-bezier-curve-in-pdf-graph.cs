using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "bezier.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size can be adjusted as needed)
            Graph graph = new Graph(500, 400);

            // Define four control points for the Bezier curve (8 float values)
            float[] controlPoints = { 100f, 300f, 150f, 350f, 250f, 250f, 300f, 300f };
            Curve bezier = new Curve(controlPoints);

            // Set the stroke color and line width for the curve
            bezier.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }
    }
}