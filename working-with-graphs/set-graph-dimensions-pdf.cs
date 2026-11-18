using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph with the exact dimensions (Width, Height)
            Graph graph = new Graph(500, 300); // dimensions in points

            // Position the graph on the page (Left, Top)
            graph.Left = 50;   // X coordinate of the lower‑left corner
            graph.Top = 750;   // Y coordinate of the upper‑left corner

            // Add a simple shape to demonstrate the graph works
            Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            shape.GraphInfo.FillColor = Color.LightGray;
            shape.GraphInfo.Color = Color.Black;
            shape.GraphInfo.LineWidth = 1;
            graph.Shapes.Add(shape);

            // Add the graph to the page content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph PDF saved to {outputPath}");
    }
}
