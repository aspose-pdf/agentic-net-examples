using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Load fonts to be used for text fragments
            Font fontArial   = FontRepository.FindFont("Arial");
            Font fontTimes   = FontRepository.FindFont("Times New Roman");
            Font fontCourier = FontRepository.FindFont("Courier New");

            // Create a Graph that covers the whole page – all drawing shapes must be added to a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height)
            {
                Left = 0,
                Top  = 0
            };

            // ---------- Rectangle node ----------
            // Draw a rectangle shape (left, bottom, width, height)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 600, 150, 100)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 2
                }
            };
            graph.Shapes.Add(rectShape);

            // Add text inside the rectangle
            TextFragment tfRect = new TextFragment("Rectangle Node")
            {
                Position = new Position(110, 660)
            };
            tfRect.TextState.Font = fontArial;
            tfRect.TextState.FontSize = 14;
            tfRect.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(tfRect);

            // ---------- Ellipse node ----------
            // Draw an ellipse shape (left, bottom, width, height)
            var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(300, 600, 150, 100)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightBlue,
                    Color     = Color.Black,
                    LineWidth = 2
                }
            };
            graph.Shapes.Add(ellipseShape);

            // Add text inside the ellipse with a different font and size
            TextFragment tfEllipse = new TextFragment("Ellipse Node")
            {
                Position = new Position(320, 660)
            };
            tfEllipse.TextState.Font = fontTimes;
            tfEllipse.TextState.FontSize = 16;
            tfEllipse.TextState.ForegroundColor = Color.DarkBlue;
            page.Paragraphs.Add(tfEllipse);

            // ---------- Connecting line ----------
            // Draw a line between rectangle and ellipse
            var lineShape = new Line(new float[] { 250, 650, 300, 650 })
            {
                GraphInfo = new GraphInfo
                {
                    Color     = Color.Black,
                    LineWidth = 1
                }
            };
            graph.Shapes.Add(lineShape);

            // ---------- Triangle node (polygon) ----------
            // Draw a triangle using a closed Line (polygon) shape
            var triangleShape = new Line(new float[] { 500, 700, 550, 600, 450, 600, 500, 700 })
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGreen,
                    Color     = Color.Black,
                    LineWidth = 2
                }
            };
            graph.Shapes.Add(triangleShape);

            // Add text inside the triangle with another font
            TextFragment tfTriangle = new TextFragment("Triangle Node")
            {
                Position = new Position(470, 640)
            };
            tfTriangle.TextState.Font = fontCourier;
            tfTriangle.TextState.FontSize = 12;
            tfTriangle.TextState.ForegroundColor = Color.Maroon;
            page.Paragraphs.Add(tfTriangle);

            // Add the graph (containing all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
