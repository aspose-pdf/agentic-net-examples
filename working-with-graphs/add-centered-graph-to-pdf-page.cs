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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a graph with desired width and height (in points)
            Graph graph = new Graph(200, 100);

            // Align the graph to the center of the page horizontally
            graph.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the graph to the first page's Paragraphs collection
            doc.Pages[1].Paragraphs.Add(graph);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}