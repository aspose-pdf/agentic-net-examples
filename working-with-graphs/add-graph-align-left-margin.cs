using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Required for TextFragment

class Program
{
    static void Main()
    {
        // Path for the output PDF file
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define graph dimensions (width, height) in points
            double graphWidth = 400;
            double graphHeight = 200;

            // Create the Graph object
            Graph graph = new Graph(graphWidth, graphHeight);

            // Align the graph to the left margin with an optional offset (e.g., 20 points)
            double leftOffset = 20; // offset from the left edge of the page
            graph.Left = leftOffset;

            // Set a title for the graph – Title expects a TextFragment, not a plain string
            graph.Title = new TextFragment("Sample Graph");

            // Style the graph border using BorderInfo
            graph.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
