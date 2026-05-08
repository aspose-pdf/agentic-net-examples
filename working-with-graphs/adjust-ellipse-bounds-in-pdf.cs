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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Retrieve page dimensions
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a Graph container that matches the page size
            Graph graph = new Graph(pageWidth, pageHeight);

            // Define an ellipse that may be partially outside the page bounds
            double ellipseLeft   = pageWidth - 50;   // intentionally near the right edge
            double ellipseBottom = pageHeight - 30; // intentionally near the top edge
            double ellipseWidth  = 100;             // width larger than remaining space
            double ellipseHeight = 80;              // height larger than remaining space

            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(
                ellipseLeft,
                ellipseBottom,
                ellipseWidth,
                ellipseHeight);

            // Check if the ellipse fits within the page bounds
            bool fits = ellipse.CheckBounds(pageWidth, pageHeight);

            if (!fits)
            {
                // Adjust position so the shape stays fully inside the page
                // Ensure left >= 0 and left + width <= pageWidth
                double adjustedLeft = Math.Max(0, Math.Min(ellipseLeft, pageWidth - ellipseWidth));
                // Ensure bottom >= 0 and bottom + height <= pageHeight
                double adjustedBottom = Math.Max(0, Math.Min(ellipseBottom, pageHeight - ellipseHeight));

                // Create a new ellipse with the adjusted coordinates
                ellipse = new Aspose.Pdf.Drawing.Ellipse(
                    adjustedLeft,
                    adjustedBottom,
                    ellipseWidth,
                    ellipseHeight);
            }

            // Optional: style the ellipse (fill and border)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}