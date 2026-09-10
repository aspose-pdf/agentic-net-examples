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

        // Load the PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape that may exceed page bounds
            float rectWidth = 100f;
            float rectHeight = 100f;
            float rectLeft = (float)(page.PageInfo.Width - 50);   // positioned near the right edge
            float rectBottom = (float)(page.PageInfo.Height - 50); // positioned near the top edge

            // Shape instance (Aspose.Pdf.Drawing.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                rectLeft,
                rectBottom,
                rectWidth,
                rectHeight);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Check if the shape fits within the page dimensions
            bool fits = rectShape.CheckBounds((float)page.PageInfo.Width, (float)page.PageInfo.Height);
            if (!fits)
            {
                // Adjust position so the shape fits (move to origin)
                rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, rectWidth, rectHeight);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
            }

            // Add the shape to the graph and the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
