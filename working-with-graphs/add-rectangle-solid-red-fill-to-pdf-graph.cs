using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – drawing objects must be placed inside a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Rectangle with absolute coordinates (llx=100, lly=500, urx=300, ury=700)
            // The constructor expects (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 200);

            // Styling via GraphInfo – solid red fill, no border
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.Red;
            rect.GraphInfo.LineWidth = 0; // removes the border

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
