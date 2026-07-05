using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_footer.pdf";

        // Use a using block to guarantee disposal of the Document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Initialise the footer – it is null by default
            page.Footer = new HeaderFooter();

            // Create a graph (width: 200 points, height: 100 points)
            Graph graph = new Graph(200.0, 100.0);

            // Position the graph in the footer area using a bottom margin
            graph.Margin = new MarginInfo { Bottom = 20 };
            // Center the graph horizontally within the footer
            graph.HorizontalAlignment = HorizontalAlignment.Center;

            // Add a simple rectangle shape to the graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Insert the graph into the page footer
            page.Footer.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
