using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_opacity.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (left, bottom, width, height)
            // Bottom = 300, Height = 200 => top = 500
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 300, 200, 200);

            // Set fill color with 50% opacity using ARGB (alpha = 128)
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255); // 50% transparent blue
            // Optional: set border color and width
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;
            rect.GraphInfo.LineWidth = 1;

            // Add the rectangle to the graph, then add the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Verify the rectangle's fill color was set (cannot access individual ARGB components directly)
            Console.WriteLine("Fill color set with 50% opacity (alpha = 128) using ARGB.");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
