using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "polygon_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph with double dimensions.
            Graph graph = new Graph(400.0, 300.0);

            // Define the polygon vertices (closed by repeating the first point).
            float[] polygonPoints = new float[]
            {
                50f,  50f,
                350f, 20f,
                380f, 200f,
                200f, 250f,
                70f,  180f,
                50f,  50f   // close the shape
            };

            // Create the polygon as a closed Line.
            Line polygon = new Line(polygonPoints);

            // Create a simple axial gradient to use as a fill pattern.
            GradientAxialShading axialShading = new GradientAxialShading(
                Color.LightGray,   // start colour
                Color.White);      // end colour
            axialShading.Start = new Point(0, 0);
            axialShading.End   = new Point(400, 300);

            // Apply styling via GraphInfo.
            polygon.GraphInfo.FillColor = new Color { PatternColorSpace = axialShading };
            polygon.GraphInfo.Color = Color.DarkBlue;   // outline colour
            polygon.GraphInfo.LineWidth = 2;            // outline thickness
            polygon.GraphInfo.DashArray = new int[] { 3, 2 }; // dash‑gap pattern

            // Add the polygon to the graph.
            graph.Shapes.Add(polygon);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with polygon saved to '{outputPath}'.");
    }
}
