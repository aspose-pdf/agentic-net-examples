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
        const float leftOffset = 20f; // offset from the left margin (points)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a Graph object (width: 400 pt, height: 200 pt) using double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);

            // Align the graph to the left margin with the specified offset
            graph.Left = leftOffset;                     // horizontal position from the left edge (float)
            graph.HorizontalAlignment = HorizontalAlignment.Left; // ensure left alignment

            // Optionally set other visual properties via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                Color = Color.Black,   // border color
                LineWidth = 1.0f       // border thickness (float literal)
            };

            // Add the graph to the first page's paragraph collection
            Page page = doc.Pages[1];
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph placed and PDF saved to '{outputPath}'.");
    }
}
