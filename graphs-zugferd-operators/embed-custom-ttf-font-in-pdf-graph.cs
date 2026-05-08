using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_custom_font.pdf";
        const string fontPath   = "custom.ttf"; // Path to your TrueType font file

        // Verify that the custom font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom TrueType font and mark it for embedding
        Font customFont = FontRepository.OpenFont(fontPath);
        customFont.IsEmbedded = true;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container for drawing shapes (use double values as required)
            Graph graph = new Graph(500.0, 300.0);

            // Draw a rectangle shape inside the graph
            // Aspose.Pdf.Drawing.Rectangle expects (float x, float y, float width, float height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // X (left)
                150f,  // Y (bottom)
                350f,  // Width  (right - left)
                100f   // Height (top - bottom)
            );
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Add a text label using the custom embedded font
            TextFragment label = new TextFragment("Sample Graph");
            label.Position = new Position(200, 260); // Position above the rectangle
            label.TextState.Font = customFont;
            label.TextState.FontSize = 24;
            label.TextState.ForegroundColor = Color.DarkBlue;
            page.Paragraphs.Add(label);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
