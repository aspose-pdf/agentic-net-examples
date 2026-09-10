using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Configure the page footer
            HeaderFooter footer = new HeaderFooter();

            // Set footer margins (using page margins as reference)
            footer.Margin = new MarginInfo
            {
                Bottom = 20,   // distance from the bottom edge of the page
                Left   = 20,   // distance from the left edge
                Right  = 20,   // distance from the right edge
                Top    = 0     // no extra top margin inside the footer area
            };

            // Create a Graph object (width, height in points)
            Graph graph = new Graph(200, 100);

            // Define visual appearance of the graph
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };

            // Add a simple rectangle shape to the graph
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 1
            };
            graph.Shapes.Add(shapeRect);

            // Add the graph to the footer's paragraph collection
            footer.Paragraphs.Add(graph);

            // Assign the configured footer to the page
            page.Footer = footer;

            // Save the PDF document
            doc.Save("GraphInFooter.pdf");
        }

        Console.WriteLine("PDF with graph in footer created successfully.");
    }
}