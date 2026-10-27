using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rounded_rectangle.pdf";

        // Ensure the output directory exists
        string outputDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold drawing shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a rectangle shape with rounded corners
            // Parameters: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 300f, 200f, 200f);
            rectShape.RoundedCornerRadius = 20f; // radius for all four corners

            // Apply a solid fill color (use Aspose.Pdf.Color)
            rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.8f); // light teal

            // Optionally set a border (stroke) color and width
            rectShape.GraphInfo.Color = Aspose.Pdf.Color.Black;
            rectShape.GraphInfo.LineWidth = 1f;

            // Add the rectangle to the graph, then add the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
    }
}
