using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_footer.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a graph (width: 200pt, height: 100pt)
            Graph graph = new Graph(200, 100);

            // Define visual appearance of the graph
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1
            };

            // Add a rectangle shape inside the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.DarkBlue,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // Configure the page footer
            page.Footer = new HeaderFooter();

            // Set footer margins relative to the page edges
            page.Footer.Margin = new MarginInfo
            {
                Top = 5,      // distance from the top of the footer area
                Bottom = 5,   // distance from the bottom of the footer area
                Left = 20,    // left margin inside the footer
                Right = 20    // right margin inside the footer
            };

            // Add the graph to the footer's paragraph collection
            page.Footer.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}