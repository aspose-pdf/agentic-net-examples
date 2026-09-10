using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_text.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph (width, height) and position it on the page
            // NOTE: use double constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 300.0)
            {
                Left = 50.0,   // distance from left edge of the page
                Top  = 500.0   // distance from bottom edge of the page
            };

            // ----- Add shapes to the graph -----

            // Rectangle shape (use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // Ellipse shape
            Ellipse ellipseShape = new Ellipse(250f, 0f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // Line shape
            float[] linePoints = { 0f, 150f, 400f, 150f };
            Line lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color     = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ----- Add text fragments with different fonts/sizes inside the shapes -----
            TextBuilder textBuilder = new TextBuilder(page);

            // Text inside the rectangle
            TextFragment rectText = new TextFragment("Rect Text");
            rectText.Position = new Position(70, 560); // adjust to fit inside rectangle
            rectText.TextState.Font = FontRepository.FindFont("Helvetica");
            rectText.TextState.FontSize = 14;
            rectText.TextState.ForegroundColor = Color.DarkBlue;
            textBuilder.AppendText(rectText);

            // Text inside the ellipse
            TextFragment ellipseText = new TextFragment("Ellipse Text");
            ellipseText.Position = new Position(300, 560); // adjust to fit inside ellipse
            ellipseText.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            ellipseText.TextState.FontSize = 12;
            ellipseText.TextState.ForegroundColor = Color.DarkRed;
            textBuilder.AppendText(ellipseText);

            // Text on the line
            TextFragment lineText = new TextFragment("Line Text");
            lineText.Position = new Position(200, 640); // position near the line
            lineText.TextState.Font = FontRepository.FindFont("Courier");
            lineText.TextState.FontSize = 10;
            lineText.TextState.ForegroundColor = Color.Green;
            textBuilder.AppendText(lineText);

            // Save the PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
