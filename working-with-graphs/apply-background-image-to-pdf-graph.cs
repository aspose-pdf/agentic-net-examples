using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string backgroundImagePath = "bg.png";
        const string outputPdfPath = "graph_with_background.pdf";

        // Ensure the background image file exists
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Apply background image to the page (acts as graph background)
            // ------------------------------------------------------------
            Image bgImage = new Image { File = backgroundImagePath };
            page.BackgroundImage = bgImage; // background image for the page

            // ------------------------------------------------------------
            // 2. Create a Graph (container for vector shapes)
            // ------------------------------------------------------------
            // Width = 400 points, Height = 200 points – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page (left, top)
                Left = 100,
                Top = 500,
                // Ensure the graph is drawn above the background image
                ZIndex = 1
            };

            // ------------------------------------------------------------
            // 3. Add a rectangle (acts as a visual element on top of the background)
            // ------------------------------------------------------------
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 300f, 150f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect); // added first – will be drawn first within the graph

            // ------------------------------------------------------------
            // 4. Add a line (drawn after the rectangle, thus on top)
            // ------------------------------------------------------------
            float[] linePoints = { 0f, 0f, 300f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 3f
            };
            graph.Shapes.Add(line); // added after rectangle, appears on top

            // ------------------------------------------------------------
            // 5. Add the graph to the page
            // ------------------------------------------------------------
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}
