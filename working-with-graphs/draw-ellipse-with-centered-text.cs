using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Draw an ellipse with a thick border and semi‑transparent fill
            // ------------------------------------------------------------
            // Graph container – size can be larger than the shape; it does not affect positioning
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(500.0, 800.0);

            // Ellipse positioned at (100, 400) with width=200 and height=100
            Ellipse ellipse = new Ellipse(100, 400, 200, 100);

            // Visual styling via GraphInfo
            // Semi‑transparent yellow fill (alpha 128 out of 255 = 50% opacity)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 255, 0), // yellow with 50% opacity
                Color = Color.Black,                         // border color
                LineWidth = 5f                               // thick border
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Place a centered TextFragment inside the ellipse
            // ------------------------------------------------------------
            TextFragment tf = new TextFragment("Centered Text")
            {
                Position = new Position(200, 450),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextState =
                {
                    FontSize = 20,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Blue
                }
            };

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // Save the resulting PDF
            // ------------------------------------------------------------
            doc.Save("EllipseWithCenteredText.pdf");
        }

        Console.WriteLine("PDF created successfully.");
    }
}
