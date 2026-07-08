using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph constructor expects double values (not float)
            Graph graph = new Graph(400.0, 300.0)
            {
                Left = 50,
                Top = 500
            };

            // ----- Add a rectangle shape -----
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };
            graph.Shapes.Add(rectShape);

            // ----- Add an ellipse shape -----
            var ellipseShape = new Ellipse(220f, 0f, 200f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph (with its shapes) to the page
            page.Paragraphs.Add(graph);

            // ----- Add text fragments inside the shapes -----
            // Text inside the rectangle
            TextFragment tfRect = new TextFragment("Rect Text")
            {
                Position = new Position(70, 540),
                TextState =
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 14,
                    ForegroundColor = Color.Blue
                }
            };

            // Text inside the ellipse
            TextFragment tfEllipse = new TextFragment("Ellipse Text")
            {
                Position = new Position(280, 540),
                TextState =
                {
                    Font = FontRepository.FindFont("TimesNewRoman"),
                    FontSize = 12,
                    ForegroundColor = Color.DarkGreen
                }
            };

            // Use TextBuilder to place the text fragments on the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tfRect);
            builder.AppendText(tfEllipse);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
