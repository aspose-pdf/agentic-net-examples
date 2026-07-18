using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_rounded_rect.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page
            Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

            // Define a rectangle shape with rounded corners
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            rectShape.RoundedCornerRadius = 15; // corner radius

            // Set visual appearance via GraphInfo (fill color, border color, line width)
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // solid fill
                Color     = Aspose.Pdf.Color.Black,    // border color
                LineWidth = 2
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rounded rectangle added and saved to '{outputPath}'.");
    }
}