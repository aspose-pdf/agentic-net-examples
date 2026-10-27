using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clipped_shapes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page to draw on
            Page page = doc.Pages.Add();

            // Define the clipping rectangle (lower‑left x, y, width, height)
            // Desired area: (100,100) to (400,400) => width = 300, height = 300
            Aspose.Pdf.Drawing.Rectangle clipRect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 300, 300);

            // Create a Graph that covers the whole page (width, height)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // NOTE: If a newer Aspose.Pdf version provides GraphInfo.Clip, you can enable clipping like this:
            // graph.GraphInfo = new GraphInfo { Clip = new ClipInfo(clipRect) };

            // Optional visual aid – draw the clipping rectangle border (red, no fill)
            var clipBorder = new Aspose.Pdf.Drawing.Rectangle(100, 100, 300, 300);
            clipBorder.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 1,
                FillColor = Color.Transparent
            };
            graph.Shapes.Add(clipBorder);

            // Rectangle that exceeds the clip bounds – only the part inside the clip will appear
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 350, 350);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Ellipse that also exceeds the clip bounds
            Ellipse ellipseShape = new Ellipse(150, 150, 350, 350);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0.2f, 0.6f, 0.9f),
                Color = Color.DarkBlue,
                LineWidth = 1,
                DashArray = new int[] { 6, 3 }
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
