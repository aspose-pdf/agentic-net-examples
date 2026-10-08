using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_branded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a Graph that matches the page size (float parameters are required)
                Graph graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

                // Create a rectangle that covers the whole page
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,                                 // left (X)
                    0f,                                 // bottom (Y)
                    (float)page.PageInfo.Width,         // width
                    (float)page.PageInfo.Height);       // height

                // Set the background fill color via GraphInfo
                rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;

                // Optional: hide the rectangle border
                rect.GraphInfo.Color = Aspose.Pdf.Color.Transparent;
                rect.GraphInfo.LineWidth = 0;

                // Add the rectangle to the graph and the graph to the page
                graph.Shapes.Add(rect);
                page.Paragraphs.Add(graph);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}
