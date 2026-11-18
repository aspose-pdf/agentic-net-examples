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
            Page page = doc.Pages[1];

            // Create a Graph that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Left = 0f;
            graph.Top = page.PageInfo.Height;

            // Configure line appearance for all shapes added to the graph
            graph.GraphInfo.LineWidth = 2f;                     // 2 points line width
            graph.GraphInfo.DashArray = new int[] { 3, 2 };    // dash: 3 on, 2 off
            graph.GraphInfo.Color = Color.Black;              // stroke colour

            // Outer border (10 points margin)
            float outerMargin = 10f;
            var outerRect = new Aspose.Pdf.Drawing.Rectangle(
                outerMargin,
                outerMargin,
                (float)page.PageInfo.Width - 2 * outerMargin,
                (float)page.PageInfo.Height - 2 * outerMargin);
            graph.Shapes.Add(outerRect);

            // Inner border (50 points margin)
            float innerMargin = 50f;
            var innerRect = new Aspose.Pdf.Drawing.Rectangle(
                innerMargin,
                innerMargin,
                (float)page.PageInfo.Width - 2 * innerMargin,
                (float)page.PageInfo.Height - 2 * innerMargin);
            graph.Shapes.Add(innerRect);

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with custom borders to '{outputPath}'.");
    }
}
