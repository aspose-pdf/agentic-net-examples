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
        const double leftOffset = 20.0; // offset from the left margin in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // Create a graph with desired width and height (e.g., 200x100 points)
            Graph graph = new Graph(200, 100);

            // Align the graph to the left margin and apply the offset
            graph.HorizontalAlignment = HorizontalAlignment.Left;
            graph.Left = leftOffset;

            // Optional visual styling via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                Color     = Color.Black, // outline color
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