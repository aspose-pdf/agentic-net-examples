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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to modify (first page in this example)
            Page page = doc.Pages[1];

            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Graph container – use the double constructor (obsolete float ctor removed)
            Graph graph = new Graph(pageWidth, pageHeight);

            // Rectangle shape that covers the whole page – use Aspose.Pdf.Drawing.Rectangle (float parameters)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)pageHeight);

            // Set visual properties via GraphInfo. Use ARGB for opacity.
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0), // 50% transparent red
                Color = Color.Transparent,                // no border color
                LineWidth = 0f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color added and saved to '{outputPath}'.");
    }
}
