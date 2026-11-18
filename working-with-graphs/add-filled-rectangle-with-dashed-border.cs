using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_rectangle.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size 500x300) – use double parameters as the float overload is obsolete
            Graph graph = new Graph(500.0, 300.0);
            // Position the graph on the page by setting its left/top coordinates (optional)
            graph.Left = 50;
            graph.Top = 500;

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 150);

            // Styling via GraphInfo
            rect.GraphInfo.FillColor = Color.LightGray;          // Fill color
            rect.GraphInfo.Color = Color.Black;                // Border color
            rect.GraphInfo.LineWidth = 2;                       // Border width
            rect.GraphInfo.DashArray = new int[] { 4, 2 };      // Dash pattern: 4 units dash, 2 units gap
            rect.GraphInfo.DashPhase = 0;                       // Optional: start offset for dash pattern

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
