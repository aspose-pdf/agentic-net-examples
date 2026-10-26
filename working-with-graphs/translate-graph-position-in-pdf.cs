using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "translated_graph.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph that covers the whole page (size can be arbitrary)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Original rectangle position and size
            const float originalLeft = 100f;
            const float originalBottom = 100f;
            const float rectWidth = 100f;
            const float rectHeight = 100f;

            // Translation offsets
            const float offsetX = 50f; // move right
            const float offsetY = 30f; // move up

            // Create a rectangle shape at the translated position
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                originalLeft + offsetX,
                originalBottom + offsetY,
                rectWidth,
                rectHeight);

            // Styling via GraphInfo
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray; // fill color
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;       // border color
            rect.GraphInfo.LineWidth = 1;

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with translated graph saved to '{outputPath}'.");
    }
}
