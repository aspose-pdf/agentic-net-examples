using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "dimension_line_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph that covers the whole page (width x height)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            page.Paragraphs.Add(graph);

            // Create a dimension‑specific line inside the graph
            // Start point (100, 100) and end point (400, 100) – coordinates are in points
            Line line = new Line(new float[] { 100, 100, 400, 100 });

            // Set line appearance via GraphInfo
            line.GraphInfo.Color = Color.FromRgb(0, 0, 1); // blue line
            line.GraphInfo.LineWidth = 2; // 2 points thick

            // Add the line to the graph's shape collection
            graph.Shapes.Add(line);

            // Optionally add a caption (e.g., length) using the Graph's Title property
            TextFragment caption = new TextFragment("Length: 300 pt")
            {
                TextState = { FontSize = 12, ForegroundColor = Color.Black }
            };
            // Assign the caption to the graph's Title (the graph does not have a Paragraphs collection)
            graph.Title = caption;

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension line saved to '{outputPath}'.");
    }
}
