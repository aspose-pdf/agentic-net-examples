using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the custom TrueType font file
        const string fontPath = "custom-font.ttf";

        // Output PDF file
        const string outputPdf = "graph_with_embedded_font.pdf";

        // Verify that the font file exists
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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create a graph (vector drawing) and apply the custom font to its text (if any)
            // -------------------------------------------------
            // Use the double‑based constructor as required by the API
            Graph graph = new Graph(400.0, 200.0);

            // Example rectangle shape inside the graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // lower‑left X
                50f,   // lower‑left Y
                300f,  // width
                150f   // height
            );
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required
                // Font cannot be set on GraphInfo; text inside the graph should be added as separate Text objects if needed
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // Add a text fragment that uses the custom embedded font
            // -------------------------------------------------
            TextFragment tf = new TextFragment("Sample text using custom font");
            tf.TextState.Font = customFont;               // apply the custom font
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.DarkBlue;
            tf.Position = new Position(100, 400);         // place the text on the page
            page.Paragraphs.Add(tf);

            // Save the document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with embedded font: {outputPdf}");
    }
}
