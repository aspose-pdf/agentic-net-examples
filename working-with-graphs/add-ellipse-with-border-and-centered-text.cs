using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document createDoc = new Document())
        {
            Page createPage = createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF and add an ellipse with a thick border and semi‑transparent fill,
        // then place a centered text fragment inside the ellipse.
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];

            // Ellipse geometry
            double left = 100.0;
            double bottom = 400.0;
            double width = 200.0;
            double height = 100.0;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            GraphInfo graphInfo = new GraphInfo();
            // Semi‑transparent red fill (alpha = 128)
            graphInfo.FillColor = Color.FromArgb(128, 255, 0, 0);
            // Thick black border
            graphInfo.LineWidth = 5f;
            graphInfo.Color = Color.Black; // stroke (border) color
            ellipse.GraphInfo = graphInfo;

            // Create a Graph container large enough to hold the ellipse
            double graphWidth = left + width + 50.0;
            double graphHeight = bottom + height + 50.0;
            Graph graph = new Graph(graphWidth, graphHeight);
            graph.Shapes.Add(ellipse);

            // Add the graph (which contains the ellipse) to the page
            page.Paragraphs.Add(graph);

            // Create a centered text fragment
            TextFragment tf = new TextFragment("Hello World");
            double centerX = left + width / 2.0;
            double centerY = bottom + height / 2.0;
            tf.Position = new Position(centerX, centerY);
            tf.HorizontalAlignment = HorizontalAlignment.Center;
            tf.TextState.FontSize = 20;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Color.Black;

            // Add text fragment to the page
            page.Paragraphs.Add(tf);

            doc.Save("output.pdf");
        }
    }
}
