using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Graph constructor now expects double values (float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0);

            // ----- Example vector shapes -----

            // Use Aspose.Pdf.Drawing.Rectangle for shapes inside a Graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Line shape
            float[] linePoints = { 200f, 150f, 350f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Add the Graph (vector graphic) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – this embeds the vector graphic directly in the page content
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded vector graphic saved to '{outputPath}'.");
    }
}
