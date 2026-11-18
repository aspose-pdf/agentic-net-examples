using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        using (var doc = new Document())
        {
            var page = doc.Pages.Add();

            // Create a graph (width, height). Position it on the page with Left/Top.
            var graph = new Graph(300, 200)
            {
                Left = 50,
                Top = 700,
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                Title = new TextFragment("Overlapping shapes demo")
            };

            // Enable bounds checking – throws if a shape does not fit into the graph area.
            graph.Shapes.UpdateBoundsCheckMode(Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit);

            // First shape – a semi‑transparent red rectangle.
            var rect = new DrawRect(20, 20, 120, 80);
            rect.GraphInfo.FillColor = Color.FromArgb(128, 255, 0, 0); // 50 % opacity
            rect.GraphInfo.Color = Color.DarkRed; // stroke colour
            rect.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(rect);

            // Second shape – a semi‑transparent blue ellipse that overlaps the rectangle.
            var ellipse = new Ellipse(80, 40, 120, 80);
            ellipse.GraphInfo.FillColor = Color.FromArgb(128, 0, 0, 255); // 50 % opacity
            ellipse.GraphInfo.Color = Color.DarkBlue;
            ellipse.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(ellipse);

            // Demonstrate bounds checking: this shape exceeds the graph area and will raise.
            try
            {
                var outOfBounds = new DrawRect(250, 150, 100, 100);
                graph.Shapes.Add(outOfBounds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bounds check triggered: {ex.GetType().Name}");
            }

            page.Paragraphs.Add(graph);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
