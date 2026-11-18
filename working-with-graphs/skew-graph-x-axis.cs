using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "skewed_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph dimensions and position on the page
            const float graphWidth = 300f;
            const float graphHeight = 100f;
            const float graphLeft = 100f;
            const float graphTop = 500f;

            // Create a Graph container (width, height) and set its location
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = graphLeft,
                Top = graphTop
            };

            // Create a horizontal line primitive inside the graph
            Line line = new Line(new float[] { 0f, 0f, graphWidth, 0f });
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Black,
                LineWidth = 2f
            };

            // Add the line to the graph's shape collection
            graph.Shapes.Add(line);

            // Apply a skew transformation on the X axis (20 degrees)
            graph.GraphInfo.SkewAngleX = 20f; // SkewAngleY remains 0 (default)

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with skewed graph saved to '{outputPath}'.");
    }
}
