using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height in points)
            Graph graph = new Graph(500, 300);

            // -------- Filled rectangle --------
            Aspose.Pdf.Drawing.Rectangle filledRect = new Aspose.Pdf.Drawing.Rectangle(50, 200, 150, 100);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,   // Fill color
                Color = Aspose.Pdf.Color.DarkBlue,       // Stroke color
                LineWidth = 2
            };
            graph.Shapes.Add(filledRect);

            // -------- Outline‑only rectangle (stroke only) --------
            Aspose.Pdf.Drawing.Rectangle outlineRect = new Aspose.Pdf.Drawing.Rectangle(250, 200, 150, 100);
            outlineRect.GraphInfo = new GraphInfo
            {
                // No FillColor => transparent (stroke only)
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2
            };
            graph.Shapes.Add(outlineRect);

            // -------- Filled ellipse --------
            Aspose.Pdf.Drawing.Ellipse filledEllipse = new Aspose.Pdf.Drawing.Ellipse(100, 50, 120, 80);
            filledEllipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Orange,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(filledEllipse);

            // -------- Outline‑only ellipse (stroke only) --------
            Aspose.Pdf.Drawing.Ellipse outlineEllipse = new Aspose.Pdf.Drawing.Ellipse(300, 50, 120, 80);
            outlineEllipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(outlineEllipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}