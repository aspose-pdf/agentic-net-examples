using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define an ellipse (left, bottom, width, height)
            // Fully qualify the type to avoid ambiguity with System.Drawing
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(100, 500, 200, 100);

            // Set the ellipse appearance – black stroke, transparent fill
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Transparent, // no fill (transparent)
                Color = Aspose.Pdf.Color.Black,           // stroke color
                LineWidth = 1
            };

            // Add the ellipse to a Graph container (required for drawing shapes)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Calculate the bounding box of the page content (includes the ellipse)
            Aspose.Pdf.Rectangle bbox = page.CalculateContentBBox();

            // Output the bounding box coordinates
            Console.WriteLine("Ellipse Bounding Box:");
            Console.WriteLine($"  Lower-Left X: {bbox.LLX}");
            Console.WriteLine($"  Lower-Left Y: {bbox.LLY}");
            Console.WriteLine($"  Upper-Right X: {bbox.URX}");
            Console.WriteLine($"  Upper-Right Y: {bbox.URY}");

            // Save the PDF
            doc.Save("EllipseWithGradient.pdf");
        }
    }
}
