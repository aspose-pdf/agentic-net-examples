using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 200)
            Graph graph = new Graph(400, 200);

            // Define four control points for the Bezier curve (8 float values)
            float[] controlPoints = new float[]
            {
                50f, 150f,   // P0
                150f, 250f,  // P1
                250f, 50f,   // P2
                350f, 150f   // P3
            };

            // Create the Curve shape with the control points
            Curve bezier = new Curve(controlPoints);

            // Set stroke color and line width via GraphInfo
            bezier.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("BezierCurve.pdf");
        }

        Console.WriteLine("PDF with Bezier curve created successfully.");
    }
}