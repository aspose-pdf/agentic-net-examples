using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container to hold vector shapes (use double constructor)
            Graph graph = new Graph(500.0, 500.0);

            // Shadow rectangle (slightly offset, semi‑transparent)
            var shadow = new Aspose.Pdf.Drawing.Rectangle(105f, 395f, 200f, 100f);
            shadow.GraphInfo = new GraphInfo
            {
                // 50% transparent gray using ARGB
                FillColor = Aspose.Pdf.Color.FromArgb(128, 128, 128, 128),
                Color = Aspose.Pdf.Color.FromArgb(128, 128, 128, 128),
                LineWidth = 0f
            };
            // Add shadow first so it appears behind the main rectangle
            graph.Shapes.Add(shadow);

            // Main rectangle (foreground)
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 400f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("RectangleWithShadow.pdf");
        }
    }
}