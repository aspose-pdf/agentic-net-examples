using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            Page page = doc.Pages[1];

            // Create a Graph that covers the whole page (or any size you need)
            // Here we use the page dimensions so we can place shapes using absolute coordinates.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (left, bottom, width, height).
            // The desired rectangle spans from (100, 400) to (300, 500).
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 400, 200, 100);

            // Configure visual appearance: 2‑point black border, dashed line.
            shapeRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Black,   // border color
                LineWidth = 2,                    // 2‑point thickness
                DashArray = new int[] { 6, 3 },   // dash pattern: 6 points on, 3 points off
                DashPhase = 0                     // start at beginning of pattern
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
