using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rounded_rectangle.pdf";

        // Document lifecycle – wrap in using for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Graph container – size matches the page dimensions
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 200, 300, 150);

            // Set corner radius
            rect.RoundedCornerRadius = 20;

            // Apply solid fill and stroke via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // solid fill
                Color = Aspose.Pdf.Color.Black,        // border color
                LineWidth = 1                           // border thickness
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
    }
}