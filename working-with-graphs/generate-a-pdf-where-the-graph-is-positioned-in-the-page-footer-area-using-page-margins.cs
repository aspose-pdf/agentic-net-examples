using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_footer.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page first – a newly created Document has no pages by default
            Page page = doc.Pages.Add();

            // Create a Footer object and assign it to the page
            page.Footer = new HeaderFooter();

            // Optional: adjust footer margins (values are in points)
            // Here we set a bottom margin of 20 points; other margins are left at default (0)
            page.Footer.Margin = new MarginInfo(0, 0, 0, 20);

            // Create a Graph object – this is a paragraph that can contain vector shapes
            // Width = 400 points, Height = 100 points (adjust as needed)
            Graph graph = new Graph(400.0, 100.0);

            // Position the graph within the footer using its own margin (optional)
            // Setting all margins to 0 makes the graph start at the left edge of the footer area
            graph.Margin = new MarginInfo(0, 0, 0, 0);

            // Use the fully‑qualified drawing rectangle to avoid the Aspose.Pdf.Rectangle ambiguity
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0.0F, 0.0F, 400.0F, 100.0F);
            rect.GraphInfo = new GraphInfo
            {
                // Fully‑qualified color to avoid System.Drawing.Color ambiguity
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Add the graph to the footer's paragraph collection
            page.Footer.Paragraphs.Add(graph);

            // Save the document (Document.Save without SaveOptions always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph in footer saved to '{outputPath}'.");
    }
}
