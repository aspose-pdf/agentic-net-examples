using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "skewed_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a graph container (width: 200 points, height: 100 points)
            Graph graph = new Graph(200, 100);

            // Create a rectangle shape inside the graph
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 70);
            // Set visual properties via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1
            };
            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Apply X‑axis skew to the entire graph (creates a slanted effect)
            // SkewAngleX is in degrees
            graph.GraphInfo.SkewAngleX = 30; // 30° skew on X axis

            // Add the graph to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Skewed graph saved to '{outputPath}'.");
    }
}