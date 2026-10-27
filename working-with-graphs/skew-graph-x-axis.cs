using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "skewed_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container with desired size
            // Width = 200, Height = 200 (you can adjust as needed)
            Graph graph = new Graph(200, 200);

            // Position the graph on the page
            graph.Left = 100;   // X‑coordinate of the left side
            graph.Top = 500;    // Y‑coordinate of the top side (from bottom of page)

            // Apply X‑axis skew (shear) – 20 degrees
            graph.GraphInfo.SkewAngleX = 20; // degrees

            // Create a rectangle shape that will act as the simple "graph"
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 200);
            rectShape.GraphInfo.FillColor = Color.LightGray; // Fill colour
            rectShape.GraphInfo.Color = Color.Black;         // Border colour
            rectShape.GraphInfo.LineWidth = 1;               // Border thickness

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Skewed graph saved to '{outputPath}'.");
    }
}
