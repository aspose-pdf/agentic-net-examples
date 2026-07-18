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

            // Create a footer and assign it to the page
            HeaderFooter footer = new HeaderFooter();

            // Set footer margins (e.g., 20 points from the bottom edge)
            footer.Margin = new MarginInfo { Bottom = 20 };

            // Attach the footer to the page
            page.Footer = footer;

            // Create a graph with a specific size (width = 200 pt, height = 100 pt)
            // Use the double‑parameter constructor as the float overload is obsolete
            Graph graph = new Graph(200.0, 100.0);

            // Position the graph within the footer using its own margins
            graph.Margin = new MarginInfo { Left = 30, Bottom = 5 };

            // Center the graph horizontally within the footer area
            graph.HorizontalAlignment = HorizontalAlignment.Center;

            // Add a rectangle shape to the graph as a visual element
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle) and float values
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the footer's paragraph collection
            footer.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("GraphInFooter.pdf");
        }
    }
}
