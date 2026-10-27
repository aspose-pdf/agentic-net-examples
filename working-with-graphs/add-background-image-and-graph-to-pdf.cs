using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_background.pdf";
        const string backgroundImagePath = "background.png"; // path to the background image

        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Add the background image as a stamp placed behind page content
            // ------------------------------------------------------------
            ImageStamp bgStamp = new ImageStamp(backgroundImagePath)
            {
                // Place the stamp behind existing content
                Background = true,
                // Stretch the image to fill the whole page
                Width = page.PageInfo.Width,
                Height = page.PageInfo.Height,
                // Align to the page origin (lower‑left corner)
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            // Add the background stamp to the page
            page.AddStamp(bgStamp);

            // ------------------------------------------------------------
            // Draw shapes on top of the background image using a Graph container
            // ------------------------------------------------------------
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Example: a red rectangle with a light‑gray fill
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 300, 200, 100); // left, bottom, width, height
            rect.GraphInfo.Color = Aspose.Pdf.Color.Red;               // border color
            rect.GraphInfo.LineWidth = 2f;                             // border thickness
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;    // fill color
            graph.Shapes.Add(rect);

            // Example: a blue ellipse with a custom fill color
            Ellipse ellipse = new Ellipse(350, 350, 200, 200); // left, bottom, width, height
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.Blue;
            ellipse.GraphInfo.LineWidth = 2f;
            ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.9f);
            graph.Shapes.Add(ellipse);

            // Example: a green line
            Line line = new Line(new float[] { 100, 300, 500, 300 }); // x1, y1, x2, y2
            line.GraphInfo.Color = Aspose.Pdf.Color.Green;
            line.GraphInfo.LineWidth = 3f;
            graph.Shapes.Add(line);

            // Add the graph (which now contains the shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
