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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a graph with desired width and height (points)
            Graph graph = new Graph(200, 100);

            // Align the graph to the center of the page (both horizontally and vertically)
            graph.HorizontalAlignment = HorizontalAlignment.Center;
            graph.VerticalAlignment   = VerticalAlignment.Center;

            // Optional: add a visible rectangle shape to the graph for demonstration
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}