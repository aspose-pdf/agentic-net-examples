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
            // Create a graph container with desired dimensions
            double graphWidth = 400;
            double graphHeight = 200;
            Graph graph = new Graph(graphWidth, graphHeight);

            // Add a sample shape (rectangle) to the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rectShape);

            // Scale all shapes by 150% using the graph's Transform via GraphInfo scaling rates
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = 1.5, // 150% scaling on X axis
                ScalingRateY = 1.5  // 150% scaling on Y axis
            };

            // Add the graph to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with scaled graph to '{outputPath}'.");
    }
}