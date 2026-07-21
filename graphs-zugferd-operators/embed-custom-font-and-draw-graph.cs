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
        const string fontPath = @"C:\Fonts\CustomFont.ttf"; // replace with actual TTF path

        // Ensure the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom TrueType font and mark it for embedding
        Font customFont = FontRepository.OpenFont(fontPath);
        customFont.IsEmbedded = true; // ensure the font is embedded in the PDF

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add a text fragment that uses the custom font
            TextFragment tf = new TextFragment("Sample Graph Title");
            tf.Position = new Position(50, 750); // place near top of page
            tf.TextState.Font = customFont;
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Color.Blue;
            page.Paragraphs.Add(tf);

            // Create a Graph container for drawing shapes (double constructor)
            Graph graph = new Graph(500.0, 300.0);

            // Rectangle shape (use Aspose.Pdf.Drawing.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 150f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Line shape
            float[] linePoints = { 300f, 50f, 450f, 150f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Ellipse shape
            var ellipse = new Ellipse(100f, 150f, 200f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.DarkGreen,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
