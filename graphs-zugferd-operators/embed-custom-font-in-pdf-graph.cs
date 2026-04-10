using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "graph_with_custom_font.pdf";
        const string customFontPath = "MyCustomFont.ttf";

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Font file not found: {customFontPath}");
            return;
        }

        // Load the custom TrueType font and mark it for embedding
        Font customFont;
        using (FileStream fontStream = File.OpenRead(customFontPath))
        {
            customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
        }
        customFont.IsEmbedded = true; // Ensure the font is embedded in the PDF

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Graph container – use double values as required by the constructor
            Graph graph = new Graph(500.0, 300.0);

            // Rectangle shape – use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Line shape
            float[] linePoints = { 300f, 200f, 450f, 250f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Add a text fragment that uses the custom embedded font
            TextFragment tf = new TextFragment("Sample text with custom font");
            tf.Position = new Position(50, 100); // Position relative to page bottom‑left
            tf.TextState.Font = customFont;      // Apply the custom font
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Color.DarkBlue;

            page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with embedded custom font.");
    }
}
