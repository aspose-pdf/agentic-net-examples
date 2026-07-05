using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPath = "graph_with_background.pdf";
        const string backgroundImagePath = "background.png";

        // Ensure the background image file exists
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Apply the background image to the page (covers the whole page)
            Image pageBg = new Image();
            pageBg.File = backgroundImagePath;
            page.BackgroundImage = pageBg;

            // Create a Graph object – width 400pt, height 200pt (use double constructor as required)
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page (left=50pt, top=600pt)
                Left = 50,
                Top = 600,
                // Ensure the graph is drawn above the page background
                ZIndex = 0
            };

            // ---- Add other shapes that should appear on top of the background image ----

            // Example: a red rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 150, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f // float literal as required
            };
            graph.Shapes.Add(rect);

            // Example: a blue line
            float[] linePoints = { 200, 150, 350, 150 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
