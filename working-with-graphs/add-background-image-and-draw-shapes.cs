using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_background.pdf";
        const string backgroundImagePath = "background.png";

        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- 1. Set the page background image ----------
            // Use Aspose.Pdf.Image (not Aspose.Pdf.Drawing.Image) for page background
            Aspose.Pdf.Image bgImg = new Aspose.Pdf.Image { File = backgroundImagePath };
            page.BackgroundImage = bgImg;

            // ---------- 2. Create a Graph (graphics container) ----------
            const double graphWidth = 400.0;   // points
            const double graphHeight = 200.0;  // points
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = 100.0,   // points from the left edge
                Top = 400.0     // points from the bottom edge
            };

            // ---------- 3. Add shapes to the graph (use fully‑qualified types) ----------
            // Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 80f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Line
            float[] linePoints = { 160f, 0f, 300f, 80f };
            var line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Ellipse
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(0f, 100f, 120f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ---------- 4. Add the graph to the page (shapes render over background) ----------
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
