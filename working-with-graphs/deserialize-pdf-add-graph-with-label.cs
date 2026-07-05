using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Try to load an existing PDF; if it does not exist, create a new one.
        Document doc;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            // Load from file (or you could load from bytes if you already have them)
            doc = new Document(inputPath);
        }
        else
        {
            // Create a fresh PDF document with a single blank page
            doc = new Document();
            doc.Pages.Add();
        }

        // Access the first page (Aspose.Pdf uses 1‑based indexing)
        Page page = doc.Pages[1];

        // Create a Graph container (width: 400, height: 200)
        Graph graph = new Graph(400.0, 200.0);

        // Draw a simple bar (rectangle) as part of the graph
        var bar = new Aspose.Pdf.Drawing.Rectangle(50f, 0f, 100f, 150f); // left, bottom, width, height
        bar.GraphInfo = new GraphInfo
        {
            FillColor = Aspose.Pdf.Color.LightBlue,
            Color = Aspose.Pdf.Color.DarkBlue,
            LineWidth = 1f
        };
        graph.Shapes.Add(bar);

        // Add the graph to the page
        page.Paragraphs.Add(graph);

        // Create a text label for the bar
        TextFragment label = new TextFragment("Sales");
        // Position the label relative to the page (below the bar). The Y coordinate is
        // calculated from the bottom of the page; adjust as needed.
        label.Position = new Position(50f, 20f);
        label.TextState.FontSize = 12;
        label.TextState.Font = FontRepository.FindFont("Helvetica");
        label.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Add the label to the page (separate from the graph)
        page.Paragraphs.Add(label);

        // Save the modified PDF
        const string outputPath = "output.pdf";
        doc.Save(outputPath);
    }
}
