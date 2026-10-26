using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_arc.pdf";

        // Ensure the output directory exists (fully‑qualified System.IO.Path to avoid ambiguity)
        string outputDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputPath))!;
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph (canvas) – use float parameters
            Graph graph = new Graph(200f, 200f)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Arc constructor expects float values
            Arc arc = new Arc(50f, 50f, 100f, 0f, 120f);

            // Fill the arc with a custom color and remove the stroke.
            arc.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0.2f, 0.7f, 0.3f), // custom greenish color
                Color     = Color.Transparent               // no stroke
            };

            // Add the arc to the graph and the graph to the page
            graph.Shapes.Add(arc);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled arc saved to '{outputPath}'.");
    }
}