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

        // Load the PDF document (using statement ensures proper disposal)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Page dimensions (container for the shape)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define initial shape dimensions
            double shapeWidth  = 200;
            double shapeHeight = 100;

            // Initial position (intentionally placed partially outside the page bounds)
            double shapeLeft   = pageWidth - 50;   // will exceed right edge
            double shapeBottom = pageHeight - 30; // will exceed top edge

            // Create a rectangle shape – constructor expects float values
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                (float)shapeLeft,
                (float)shapeBottom,
                (float)shapeWidth,
                (float)shapeHeight);
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float literal
            };

            // Check if the shape fits within the page bounds
            bool fits = rectShape.CheckBounds(pageWidth, pageHeight);
            if (!fits)
            {
                // Adjust position so the shape stays completely inside the page
                double adjustedLeft   = Math.Max(0, Math.Min(shapeLeft,   pageWidth  - shapeWidth));
                double adjustedBottom = Math.Max(0, Math.Min(shapeBottom, pageHeight - shapeHeight));

                // Recreate the rectangle with adjusted coordinates (float parameters)
                rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)adjustedLeft,
                    (float)adjustedBottom,
                    (float)shapeWidth,
                    (float)shapeHeight);
                rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };
            }

            // Create a Graph container and add the shape
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
