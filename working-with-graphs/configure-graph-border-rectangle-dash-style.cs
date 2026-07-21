using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_border.pdf";

        // Create a new PDF document (lifecycle rule: create)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold drawing shapes
            // Width = 500 points, Height = 300 points (use double as required by the constructor)
            Graph graph = new Graph(500.0, 300.0);

            // Configure the Graph's outer border (optional)
            // Set border on all sides, width 2 points, black color
            graph.Border = new BorderInfo(
                BorderSide.Left | BorderSide.Top | BorderSide.Right | BorderSide.Bottom,
                2f,
                Aspose.Pdf.Color.Black);

            // Create a rectangle shape (positioned at (50,50) with width 200 and height 150)
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 200f, 150f);

            // Configure rectangle visual properties via GraphInfo before adding to the graph
            rect.GraphInfo = new GraphInfo
            {
                // Set line width for the rectangle border
                LineWidth = 3f,
                // Set border color
                Color = Aspose.Pdf.Color.Blue,
                // Define dash pattern: 5 points dash, 3 points gap
                DashArray = new int[] { 5, 3 }
            };

            // Add the rectangle to the Graph's shape collection
            graph.Shapes.Add(rect);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document to a file (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
