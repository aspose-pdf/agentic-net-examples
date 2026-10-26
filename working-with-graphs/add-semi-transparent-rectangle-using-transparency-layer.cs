using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "transparent_shape.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page (use double overload – more stable)
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            var rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 300, 600);
            // Fill with semi‑transparent blue (alpha 128 ≈ 50% opacity)
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255);
            // Optional border settings
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;
            rect.GraphInfo.LineWidth = 1;

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
