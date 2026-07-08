using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_shadow.pdf";

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define rectangle position and size
            float left = 100f;
            float bottom = 500f;
            float width = 200f;
            float height = 100f;

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ----- Shadow rectangle (slightly offset, semi‑transparent) -----
            // Offset by 5 points to the right and down to simulate a shadow
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(left + 5f, bottom - 5f, width, height);
            shadowRect.GraphInfo = new GraphInfo
            {
                // 50 % transparent black fill for the shadow
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 0),
                // No stroke outline
                Color = Aspose.Pdf.Color.Transparent,
                LineWidth = 0f
            };
            graph.Shapes.Add(shadowRect);

            // ----- Main rectangle (on top of the shadow) -----
            var mainRect = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height);
            mainRect.GraphInfo = new GraphInfo
            {
                // Light‑blue fill for the visible rectangle
                FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.9f),
                // Black border
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(mainRect);

            // Add the graph (containing both rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the document to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rectangle and shadow saved to '{outputPath}'.");
    }
}
