using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Added for TextFragment

class ExportGraphToPdf
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph object with desired width and height (in points)
            // Use double values as the constructor overload with float is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Example: add a rectangle shape to the graph
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            // Set visual properties via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Optionally set a title for the graph (expects a TextFragment)
            graph.Title = new TextFragment("Sample Graph");

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to PDF: {outputPath}");
    }
}
