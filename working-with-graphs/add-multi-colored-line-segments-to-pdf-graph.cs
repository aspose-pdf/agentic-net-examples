using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) that will hold the line segments
            Graph graph = new Graph(500, 300);

            // ----- Segment 1 (red) -----
            // Define start (50,250) and end (150,150) points
            float[] seg1 = { 50, 250, 150, 150 };
            Line line1 = new Line(seg1);
            line1.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2
            };
            graph.Shapes.Add(line1);

            // ----- Segment 2 (green) -----
            // Continue from previous end point to a new point (250,200)
            float[] seg2 = { 150, 150, 250, 200 };
            Line line2 = new Line(seg2);
            line2.GraphInfo = new GraphInfo
            {
                Color = Color.Green,
                LineWidth = 2
            };
            graph.Shapes.Add(line2);

            // ----- Segment 3 (blue) -----
            // Continue to another point (350,100)
            float[] seg3 = { 250, 200, 350, 100 };
            Line line3 = new Line(seg3);
            line3.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line3);

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
    }
}