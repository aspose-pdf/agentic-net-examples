using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500, height: 300)
            Graph graph = new Graph(500, 300);

            // ----- First line segment (red) -----
            // Define start (50,250) and end (250,150) points
            float[] line1Pos = { 50, 250, 250, 150 };
            Line line1 = new Line(line1Pos);
            line1.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2
            };
            graph.Shapes.Add(line1);

            // ----- Second line segment (blue) -----
            // Define start (250,150) and end (450,200) points
            float[] line2Pos = { 250, 150, 450, 200 };
            Line line2 = new Line(line2Pos);
            line2.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line2);

            // Add the Graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}