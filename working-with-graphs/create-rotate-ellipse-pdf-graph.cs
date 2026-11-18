using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph object that will hold drawing primitives.
            // Constructor takes only width and height.
            Graph graph = new Graph(300, 300);
            // Position the graph on the page (optional – defaults to (0,0)).
            graph.Left = 0;
            graph.Top = 0;

            // Optional: give the graph a thin border for visibility
            graph.Border = new BorderInfo(BorderSide.All, 0.5f);

            // Create an ellipse shape.
            // Parameters: X, Y (lower‑left corner of bounding box), width, height.
            Ellipse ellipse = new Ellipse(50, 50, 200, 100);

            // Set fill and stroke colours via GraphInfo.
            ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue;
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.Black; // optional stroke colour

            // Rotate the ellipse 45° around its own centre using GraphInfo.RotationAngle (degrees).
            ellipse.GraphInfo.RotationAngle = 45;

            // Add the ellipse to the graph's shape collection.
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated ellipse saved to '{outputPath}'.");
    }
}
