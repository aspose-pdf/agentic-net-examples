using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_shapes.pdf";

        // Ensure the output directory exists
        string outDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (the area where drawing is allowed)
            // Note: Graph does not expose a Clip property; this rectangle is kept for reference.
            Aspose.Pdf.Rectangle clipRect = new Aspose.Pdf.Rectangle(100f, 100f, 300f, 300f);

            // Create a Graph that covers the whole page (Graph(width, height))
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Left = 0;               // position on the page
            graph.Top = 0;
            // graph.Clip = clipRect; // Graph has no Clip property – omitted

            // Draw a large rectangle that exceeds the clip area
            Aspose.Pdf.Drawing.Rectangle largeRect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 500f, 500f);
            largeRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
            largeRect.GraphInfo.Color = Aspose.Pdf.Color.Black;
            largeRect.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(largeRect);

            // Draw an ellipse that also extends beyond the clip area
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200.0, 200.0, 600.0, 600.0);
            ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.8f);
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.DarkBlue;
            ellipse.GraphInfo.LineWidth = 1f;
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
