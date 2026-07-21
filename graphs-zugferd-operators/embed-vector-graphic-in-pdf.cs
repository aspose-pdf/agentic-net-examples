using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_embedded.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) using double literals as required
            Graph graph = new Graph(400.0, 200.0);

            // Draw a rectangle (left, bottom, width, height) – use Aspose.Pdf.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 300f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Draw a line using a float array { x1, y1, x2, y2 }
            float[] linePoints = { 50f, 150f, 350f, 250f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Embed the vector graphic into the page by adding the Graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded vector graphic saved to '{outputPath}'.");
    }
}
