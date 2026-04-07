using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Graph constructor now expects double values
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page
                Left = 50,
                Top = 500
            };

            // ----------- Filled rectangle -----------
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var filledRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // fill color
                Color = Aspose.Pdf.Color.Black,        // stroke color
                LineWidth = 1f
            };
            graph.Shapes.Add(filledRect);

            // ----------- Unfilled circle (stroke only) -----------
            var outlineCircle = new Circle(150f, 25f, 30f);
            outlineCircle.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue, // stroke color
                LineWidth = 2f                 // thicker outline
                // No FillColor set → outline only
            };
            graph.Shapes.Add(outlineCircle);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
