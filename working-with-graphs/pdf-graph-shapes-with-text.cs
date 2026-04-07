using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double values as required by the constructor
            Graph graph = new Graph(500.0, 300.0)
            {
                // Position the graph on the page (coordinates are from bottom‑left)
                Left = 50,
                Top  = 500
            };

            // ---------- Shape 1: Rectangle (drawing rectangle) ----------
            // Aspose.Pdf.Drawing.Rectangle constructor: (left, bottom, width, height) – coordinates are relative to the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // Text inside the rectangle
            TextFragment rectText = new TextFragment("Aspose.Pdf.Rectangle Text")
            {
                // Position is absolute on the page
                Position = new Position(70, 560) // left + 20, bottom + 60 (approx.)
            };
            rectText.TextState.Font = FontRepository.FindFont("Helvetica");
            rectText.TextState.FontSize = 12;
            rectText.TextState.ForegroundColor = Color.Blue;

            // ---------- Shape 2: Ellipse ----------
            // Ellipse constructor: (left, bottom, width, height)
            Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250f, 0f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // Text inside the ellipse
            TextFragment ellipseText = new TextFragment("Ellipse Text")
            {
                Position = new Position(300, 560) // approximate center of the ellipse
            };
            ellipseText.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            ellipseText.TextState.FontSize = 14;
            ellipseText.TextState.ForegroundColor = Color.DarkGreen;

            // Add the graph (with shapes) to the page
            page.Paragraphs.Add(graph);

            // Add the text fragments to the page (they will appear over the shapes)
            page.Paragraphs.Add(rectText);
            page.Paragraphs.Add(ellipseText);

            // Save the PDF – no SaveOptions needed for PDF output
            doc.Save("graph_with_text.pdf");
        }

        Console.WriteLine("PDF generated: graph_with_text.pdf");
    }
}
