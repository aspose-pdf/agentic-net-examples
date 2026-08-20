using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Path to the output PDF
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points)
            Graph graph = new Graph(400, 200);

            // Define a rectangle shape: left=50, bottom=50, width=300, height=100
            // This uses Aspose.Pdf.Drawing.Rectangle (shape), not Aspose.Pdf.Rectangle (page rectangle)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 100);

            // Set corner radius for rounded corners
            rect.RoundedCornerRadius = 15; // radius in points

            // Set visual appearance via GraphInfo (fill color, border color, line width)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // solid fill color
                Color = Aspose.Pdf.Color.DarkBlue,      // border (stroke) color
                LineWidth = 2                            // border thickness
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rectangle saved to '{outputPath}'.");
    }
}