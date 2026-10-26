using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Offset from the left margin (in points)
        const double leftOffset = 50;
        // Desired size of the graph
        const double graphWidth = 400;
        const double graphHeight = 300;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Create a Graph with the required size
            Graph graph = new Graph(graphWidth, graphHeight);
            // Position the graph: left offset from the page left edge and top offset from the page top edge
            graph.Left = leftOffset;
            // In Aspose.Pdf the Y‑coordinate is measured from the bottom, so we subtract the offset from the page height
            graph.Top = page.PageInfo.Height - leftOffset;

            // Optional: add a title to the graph
            graph.Title = new TextFragment("Sample Chart");

            // Add a simple shape (a bar) to illustrate the graph content
            // Use fully‑qualified type to avoid ambiguity between Aspose.Pdf.Rectangle and Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(20f, 0f, 40f, 120f);
            bar.Text = new TextFragment("Q1");
            bar.GraphInfo.FillColor = Color.SteelBlue;
            bar.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(bar);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}
