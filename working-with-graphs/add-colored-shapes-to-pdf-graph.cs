using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Map shape identifiers to fill colors
        var fillColors = new Dictionary<string, Color>
        {
            { "rect1", Color.LightGray },
            { "ellipse1", Color.Yellow },
            { "line1", Color.Red } // lines use the Color property (stroke)
        };

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to draw on
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a Graph container (size in points)
            Graph graph = new Graph(400, 200);

            // ---------- Rectangle ----------
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 150, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = fillColors["rect1"], // fill color from dictionary
                Color = Color.Black,             // border color
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ---------- Ellipse ----------
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(250, 150, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = fillColors["ellipse1"], // fill color from dictionary
                Color = Color.Red,                  // border color
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---------- Line ----------
            float[] linePoints = { 50, 50, 350, 50 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = fillColors["line1"], // stroke color from dictionary
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}