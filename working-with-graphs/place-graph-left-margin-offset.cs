using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const double offset     = 50; // points from the left margin

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a graph of width 200 and height 100 points
            Graph graph = new Graph(200, 100);

            // Align the graph to the left margin with the specified offset
            graph.Left = offset;   // horizontal position
            graph.Top  = 500;      // vertical position (distance from bottom)

            // Optional visual styling via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                Color     = Color.Black,
                LineWidth = 1
            };

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph placed and saved to '{outputPath}'.");
    }
}