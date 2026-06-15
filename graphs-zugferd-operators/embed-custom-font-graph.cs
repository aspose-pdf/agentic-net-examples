using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: create a simple PDF and save it as a sample input file
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Step 2: reopen the sample PDF, add a graph and embed a TrueType font for text
        using (Document doc = new Document("input.pdf"))
        {
            // Load a TrueType font (e.g., Arial) and mark it to be embedded
            Font customFont = FontRepository.FindFont("Arial");
            customFont.IsEmbedded = true;

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a graph canvas
            Graph graph = new Graph(400f, 200f);

            // Add a rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);
            rect.GraphInfo.FillColor = Color.FromRgb(System.Drawing.Color.LightBlue);
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Add a text fragment that uses the embedded font
            TextFragment text = new TextFragment("Hello with embedded font");
            text.TextState.Font = customFont;
            text.TextState.FontSize = 24;
            text.Position = new Position(60f, 150f);
            page.Paragraphs.Add(text);

            // Save the final PDF
            doc.Save("output.pdf");
        }
    }
}