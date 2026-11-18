using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Retrieve the page dimensions (width and height)
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph object that matches the page size.
            // Graph constructor expects width and height (float values).
            Graph graph = new Graph((float)pageWidth, (float)pageHeight);

            // OPTIONAL: draw a sample line inside the graph to demonstrate usage.
            // The Line constructor takes a float array: [x1, y1, x2, y2].
            Line line = new Line(new float[] { 100f, 100f, 300f, 200f });
            line.GraphInfo.Color = Color.Black; // set line colour via GraphInfo
            graph.Shapes.Add(line);

            // Add the Graph to the page's content.
            page.Paragraphs.Add(graph);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Graph saved to '{outputPath}'.");
    }
}
