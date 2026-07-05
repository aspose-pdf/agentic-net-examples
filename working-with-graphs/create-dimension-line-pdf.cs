using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "dimension_line.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt)
            Graph graph = new Graph(400, 200);

            // Define a line from (50,150) to (350,150)
            float[] linePositions = { 50f, 150f, 350f, 150f };
            Line line = new Line(linePositions);

            // Set line visual properties via GraphInfo
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Black,   // line color
                LineWidth = 2f         // line thickness
            };

            // Add the line to the graph's shape collection
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}