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

        using (Document doc = new Document(inputPath))
        {
            // Graph constructor expects double values for width and height
            Graph graph = new Graph(400.0, 200.0);

            // Drawing rectangle uses float parameters
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 150f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // interior fill
                Color = Aspose.Pdf.Color.Blue,          // border color
                LineWidth = 2f,                         // border thickness
                // DashArray property expects an int[] array
                DashArray = new int[] { 5, 3 }          // dash length 5, gap 3
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the first page of the document
            doc.Pages[1].Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}