using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_text.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph with double dimensions
            Graph graph = new Graph(300.0, 200.0);

            // Position the graph on the page
            graph.Left = 100;   // X coordinate
            graph.Top = 400;    // Y coordinate (top of the graph)

            // Add a rectangle shape inside the graph
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 300, 200);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Add text inside the graph at specific coordinates
            TextFragment tf = new TextFragment("Sample Text Inside Graph");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the text (coordinates are relative to the page)
            tf.Position = new Position(150, 500);

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
