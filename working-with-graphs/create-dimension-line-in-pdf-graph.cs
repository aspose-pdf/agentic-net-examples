using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "dimension_line.pdf";

        // Ensure the output directory exists
        string outDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the drawing
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – all drawing objects must be placed inside a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the line coordinates (x1, y1, x2, y2)
            // Example: a 200‑point long horizontal line starting at (100,500)
            Line line = new Line(new float[] { 100f, 500f, 300f, 500f });
            // Set visual properties via GraphInfo
            line.GraphInfo.Color = Color.Black;
            line.GraphInfo.LineWidth = 2f;

            // Add the line to the graph and the graph to the page
            graph.Shapes.Add(line);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension‑specific line saved to '{outputPath}'.");
    }
}
