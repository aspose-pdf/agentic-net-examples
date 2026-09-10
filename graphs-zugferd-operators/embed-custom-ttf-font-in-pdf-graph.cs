using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_custom_font.pdf";

        // Load a TrueType font that is available on the system and ensure it is embedded.
        // Using FontRepository.FindFont avoids the need for an external file.
        Font customFont = FontRepository.FindFont("Arial"); // replace "Arial" with any installed TTF font name if needed
        customFont.IsEmbedded = true; // ensure embedding

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a graph (container for drawing shapes)
            Graph graph = new Graph(400.0, 200.0); // use double values as required by the API

            // Define a rectangle shape for the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 300f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Add a text fragment using the custom font
            TextFragment tf = new TextFragment("Custom Font Text");
            tf.TextState.Font = customFont;
            tf.TextState.FontSize = 24;
            tf.Position = new Position(100, 300); // position on the page
            page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with embedded custom TrueType font.");
    }
}
