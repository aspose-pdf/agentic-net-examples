using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Common colors for shapes and borders
            Color shapeFill   = Color.LightGray;
            Color shapeBorder = Color.DarkGray;

            // -------------------------------------------------
            // Create a Graph container – all drawing shapes (circles, rectangles) go here
            // -------------------------------------------------
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // -------------------
            // Draw three circles (nodes of a graph) with text inside
            // -------------------

            // Circle 1
            Circle circle1 = new Circle(100, 600, 50);
            circle1.GraphInfo.FillColor = shapeFill;          // fill
            circle1.GraphInfo.Color    = shapeBorder;        // border color
            circle1.GraphInfo.LineWidth = 1;                  // border thickness
            graph.Shapes.Add(circle1);

            // Text inside Circle 1
            TextFragment tf1 = new TextFragment("A");
            tf1.TextState.Font = FontRepository.FindFont("Arial");
            tf1.TextState.FontSize = 24;
            tf1.TextState.ForegroundColor = Color.Blue;
            tf1.Position = new Position(115, 610); // roughly centered
            page.Paragraphs.Add(tf1);

            // Circle 2
            Circle circle2 = new Circle(250, 600, 70);
            circle2.GraphInfo.FillColor = shapeFill;
            circle2.GraphInfo.Color    = shapeBorder;
            circle2.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(circle2);

            // Text inside Circle 2
            TextFragment tf2 = new TextFragment("B");
            tf2.TextState.Font = FontRepository.FindFont("Times New Roman");
            tf2.TextState.FontSize = 30;
            tf2.TextState.ForegroundColor = Color.Red;
            tf2.Position = new Position(260, 610);
            page.Paragraphs.Add(tf2);

            // Circle 3
            Circle circle3 = new Circle(420, 600, 60);
            circle3.GraphInfo.FillColor = shapeFill;
            circle3.GraphInfo.Color    = shapeBorder;
            circle3.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(circle3);

            // Text inside Circle 3
            TextFragment tf3 = new TextFragment("C");
            tf3.TextState.Font = FontRepository.FindFont("Courier New");
            tf3.TextState.FontSize = 20;
            tf3.TextState.ForegroundColor = Color.Green;
            tf3.Position = new Position(430, 610);
            page.Paragraphs.Add(tf3);

            // -------------------------------------------------
            // Simple bar chart (edges of the graph) using rectangles
            // -------------------------------------------------

            // Bar 1
            Aspose.Pdf.Drawing.Rectangle bar1 = new Aspose.Pdf.Drawing.Rectangle(100, 300, 50, 200); // left, bottom, width, height
            bar1.GraphInfo.FillColor = Color.FromRgb(0.2, 0.6, 0.8);
            bar1.GraphInfo.Color    = Color.Black;
            bar1.GraphInfo.LineWidth = 0.5f;
            graph.Shapes.Add(bar1);
            // Label for Bar 1
            TextFragment bar1Label = new TextFragment("Bar 1");
            bar1Label.TextState.Font = FontRepository.FindFont("Helvetica");
            bar1Label.TextState.FontSize = 12;
            bar1Label.TextState.ForegroundColor = Color.Black;
            bar1Label.Position = new Position(110, 280);
            page.Paragraphs.Add(bar1Label);

            // Bar 2
            Aspose.Pdf.Drawing.Rectangle bar2 = new Aspose.Pdf.Drawing.Rectangle(200, 200, 50, 300);
            bar2.GraphInfo.FillColor = Color.FromRgb(0.8, 0.4, 0.2);
            bar2.GraphInfo.Color    = Color.Black;
            bar2.GraphInfo.LineWidth = 0.5f;
            graph.Shapes.Add(bar2);
            // Label for Bar 2
            TextFragment bar2Label = new TextFragment("Bar 2");
            bar2Label.TextState.Font = FontRepository.FindFont("Helvetica");
            bar2Label.TextState.FontSize = 12;
            bar2Label.TextState.ForegroundColor = Color.Black;
            bar2Label.Position = new Position(210, 180);
            page.Paragraphs.Add(bar2Label);

            // Bar 3
            Aspose.Pdf.Drawing.Rectangle bar3 = new Aspose.Pdf.Drawing.Rectangle(300, 250, 50, 250);
            bar3.GraphInfo.FillColor = Color.FromRgb(0.4, 0.8, 0.3);
            bar3.GraphInfo.Color    = Color.Black;
            bar3.GraphInfo.LineWidth = 0.5f;
            graph.Shapes.Add(bar3);
            // Label for Bar 3
            TextFragment bar3Label = new TextFragment("Bar 3");
            bar3Label.TextState.Font = FontRepository.FindFont("Helvetica");
            bar3Label.TextState.FontSize = 12;
            bar3Label.TextState.ForegroundColor = Color.Black;
            bar3Label.Position = new Position(310, 230);
            page.Paragraphs.Add(bar3Label);

            // Add the graph (containing all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF generated: {outputPath}");
    }
}
