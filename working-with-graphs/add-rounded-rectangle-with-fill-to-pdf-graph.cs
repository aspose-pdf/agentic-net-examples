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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container with the desired size
            Graph graph = new Graph(200f, 100f);
            // Position the graph on the page (lower‑left corner at (100, 500))
            graph.Left = 100f;
            graph.Top = 500f;

            // Create a rectangle shape inside the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            // Set corner radius (rounded corners)
            rectShape.RoundedCornerRadius = 15f;

            // Apply styling via GraphInfo
            rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.9f); // light blue fill
            rectShape.GraphInfo.Color = Aspose.Pdf.Color.Black;                     // optional border colour
            rectShape.GraphInfo.LineWidth = 1f;                                      // optional border width

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
