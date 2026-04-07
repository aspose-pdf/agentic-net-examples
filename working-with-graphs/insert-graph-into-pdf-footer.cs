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
            // Add a single page
            Page page = doc.Pages.Add();

            // Configure the page footer
            HeaderFooter footer = new HeaderFooter();
            // Set a bottom margin of 20 points for the footer area
            footer.Margin = new MarginInfo(0, 0, 0, 20);
            page.Footer = footer;

            // Determine the usable width of the page
            double pageWidth = page.PageInfo.Width;
            // Height of the graph that will sit in the footer
            double graphHeight = 30;

            // Create a Graph sized to the page width and desired height
            Graph graph = new Graph(pageWidth, graphHeight);

            // Create a rectangle shape for the graph (use Aspose.Pdf.Drawing.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)graphHeight);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the footer
            footer.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("GraphInFooter.pdf");
        }
    }
}
