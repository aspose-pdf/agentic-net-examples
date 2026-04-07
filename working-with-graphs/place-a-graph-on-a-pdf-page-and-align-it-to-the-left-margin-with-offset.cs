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

        // Desired graph size (points) and left offset from the page margin
        const double graphWidth  = 400;
        const double graphHeight = 200;
        const double leftOffset  = 50; // offset from the left edge

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a Graph object with the specified width and height
            Graph graph = new Graph(graphWidth, graphHeight);

            // Align the graph to the left margin and apply the offset
            graph.HorizontalAlignment = HorizontalAlignment.Left;
            graph.Left = leftOffset; // distance from the left edge of the page

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph placed and saved to '{outputPath}'.");
    }
}