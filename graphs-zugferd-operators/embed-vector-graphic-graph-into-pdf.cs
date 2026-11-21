using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.IO;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph object (vector graphic) with double‑precision dimensions
            Graph vectorGraph = new Graph(500.0, 400.0)
            {
                Left = 0.0,
                Top = 0.0
            };

            // Helper to create a line shape with a given set of points
            Line CreateLine(float[] points)
            {
                var line = new Line(points);
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Black,
                    FillColor = Color.Transparent
                };
                return line;
            }

            // X‑axis
            vectorGraph.Shapes.Add(CreateLine(new float[] { 50, 350, 450, 350 }));
            // Y‑axis
            vectorGraph.Shapes.Add(CreateLine(new float[] { 50, 50, 50, 350 }));
            // Simple data series (poly‑line)
            vectorGraph.Shapes.Add(CreateLine(new float[] { 50, 350, 100, 300, 150, 320, 200, 280, 250, 310, 300, 260 }));

            // Embed the graph directly into the page
            page.Paragraphs.Add(vectorGraph);

            // Save the PDF; no SaveOptions needed for PDF output
            doc.Save(outputPath);
        }

        // Use fully‑qualified System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"PDF with embedded vector graphic saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
