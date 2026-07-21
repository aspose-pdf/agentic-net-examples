using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        var borderColors = new Dictionary<int, Aspose.Pdf.Color>
        {
            { 0, Aspose.Pdf.Color.FromRgb(1, 0, 0) }, // Red
            { 1, Aspose.Pdf.Color.FromRgb(0, 1, 0) }, // Green
            { 2, Aspose.Pdf.Color.FromRgb(0, 0, 1) }, // Blue
            { 3, Aspose.Pdf.Color.FromRgb(1, 1, 0) }  // Yellow
        };

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (new API)
            Graph graph = new Graph(500.0, 400.0);
            graph.Left = 50.0;
            graph.Top = 500.0;

            // Shape 0: Rectangle (use Drawing.Rectangle, not Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = GetBorderColor(borderColors, 0),
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Shape 1: Ellipse
            var ellipse = new Ellipse(250f, 0f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color = GetBorderColor(borderColors, 1),
                LineWidth = 2f
            };
            graph.Shapes.Add(ellipse);

            // Shape 2: Line
            float[] linePoints = { 0f, 150f, 300f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = GetBorderColor(borderColors, 2),
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Shape 3: Second rectangle
            var rect2 = new Aspose.Pdf.Drawing.Rectangle(0f, 200f, 200f, 100f);
            rect2.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGreen,
                Color = GetBorderColor(borderColors, 3),
                LineWidth = 2f
            };
            graph.Shapes.Add(rect2);

            page.Paragraphs.Add(graph);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }

    // Helper to fetch border color; defaults to Black if not found
    static Aspose.Pdf.Color GetBorderColor(Dictionary<int, Aspose.Pdf.Color> map, int index)
    {
        return map.TryGetValue(index, out Aspose.Pdf.Color color) ? color : Aspose.Pdf.Color.Black;
    }
}