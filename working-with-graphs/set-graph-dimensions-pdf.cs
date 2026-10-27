using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_dimensions.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the double‑based constructor for Graph (width, height).
            Graph graph = new Graph(400.0, 300.0);

            // Create a line that fills the graph area.
            // The Line constructor expects a float array, so cast the double values.
            Line line = new Line(new float[] { 0f, 0f, (float)graph.Width, (float)graph.Height });
            // Set the line colour via GraphInfo.
            line.GraphInfo.Color = Aspose.Pdf.Color.Blue;
            graph.Shapes.Add(line);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}