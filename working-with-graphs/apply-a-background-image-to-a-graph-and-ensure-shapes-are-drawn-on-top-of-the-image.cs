using System;
using System.IO;
using System.Drawing; // for loading the background image
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_background.pdf";
        const string bgImagePath = "background.png";

        // Verify that the background image file exists.
        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // Create a Graph container (width = 400pt, height = 300pt).
            // Use the double‑based constructor as the float overload is obsolete.
            Graph graph = new Graph(400.0, 300.0)
            {
                // Position the graph on the page.
                Left = 50.0,
                Top  = 500.0
            };

            // ------------------------------------------------------------
            // 1. Add the background image as the first shape in the graph.
            // ------------------------------------------------------------
            // Load the image with System.Drawing.Image and pass it to Aspose.Pdf.Drawing.Image.
            System.Drawing.Image sysImg = System.Drawing.Image.FromFile(bgImagePath);
            Aspose.Pdf.Drawing.Image bgShape = new Aspose.Pdf.Drawing.Image(sysImg);
            // Set the rectangle so the image fills the whole graph area.
            bgShape.Rectangle = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 400f, 300f);
            // Add the background image shape.
            graph.Shapes.Add(bgShape);

            // ------------------------------------------------------------
            // 2. Add other shapes that should appear on top of the image.
            // ------------------------------------------------------------

            // Example: a filled rectangle.
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 200f, 100f, 100f); // left, bottom, width, height
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Example: a red line.
            float[] linePoints = { 200f, 250f, 350f, 250f }; // x1, y1, x2, y2
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 3f
            };
            graph.Shapes.Add(line);

            // Add the fully built graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
