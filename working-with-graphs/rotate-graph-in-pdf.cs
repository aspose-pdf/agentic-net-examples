using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a Graph container (width, height)
            Graph graph = new Graph(200, 200);

            // Apply rotation in degrees (e.g., 45°) via GraphInfo.RotationAngle
            graph.GraphInfo.RotationAngle = 45; // degrees

            // Add a sample rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Add the graph to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(graph);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated graph PDF saved to '{outputPath}'.");
    }
}