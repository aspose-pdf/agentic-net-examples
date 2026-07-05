using System;
using System.IO;
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

            // Define four control points for the Bezier curve (x0,y0, x1,y1, x2,y2, x3,y3)
            // The Curve class expects a float array of length 8 (four points)
            float[] controlPoints = new float[]
            {
                100f, 700f,   // Start point (P0)
                150f, 800f,   // First control point (P1)
                250f, 600f,   // Second control point (P2)
                300f, 700f    // End point (P3)
            };

            // Create the curve shape
            Curve bezier = new Curve(controlPoints);

            // Set stroke color and line width via GraphInfo
            bezier.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,   // Stroke color
                LineWidth = 2                     // Optional line width
            };

            // Create a Graph container (size can be the page size)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Add the curve to the graph
            graph.Shapes.Add(bezier);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("BezierCurve.pdf");
        }

        Console.WriteLine("Bezier curve PDF created successfully.");
    }
}