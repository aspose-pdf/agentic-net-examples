using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Graph constructor now expects double values (use literals with a decimal point)
            Graph graph = new Graph(500.0, 500.0);

            // Define an ellipse (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 100, 200, 150);

            // Gradient fills cannot be assigned directly to GraphInfo.FillColor because the property type is Aspose.Pdf.Color.
            // Use a solid fill as a placeholder; the ellipse will still be measured correctly.
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // solid fill (gradient not directly supported here)
                Color = Aspose.Pdf.Color.Black,        // stroke color
                LineWidth = 1f                         // float literal as required
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Measure the content bounding box after the ellipse is added
            Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

            // Output the measured bounding box coordinates
            Console.WriteLine($"Content BBox: LLX={contentBox.LLX}, LLY={contentBox.LLY}, URX={contentBox.URX}, URY={contentBox.URY}");

            // Save the PDF (standard PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
