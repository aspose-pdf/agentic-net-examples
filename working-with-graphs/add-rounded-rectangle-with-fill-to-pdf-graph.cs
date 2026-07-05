using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) that will hold vector shapes
            Graph graph = new Graph(400, 200);
            // Position the graph on the page (optional)
            graph.Left = 100;
            graph.Top = 500;

            // Create a rectangle shape within the graph
            // Parameters: left, bottom, width, height (all in points)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);

            // Set the corner radius for rounded corners
            rect.RoundedCornerRadius = 15;

            // Define visual styling via GraphInfo (fill color, border color, line width)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // solid fill color
                Color = Aspose.Pdf.Color.Black,        // border color (optional)
                LineWidth = 1                           // border thickness (optional)
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph (with the rectangle) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}