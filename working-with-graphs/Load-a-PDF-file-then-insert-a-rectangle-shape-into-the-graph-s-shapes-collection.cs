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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container (size in points)
            Graph graph = new Graph(400, 200); // width, height

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            // Set visual properties via GraphInfo (no direct FillColor, etc.)
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}