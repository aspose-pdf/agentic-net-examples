using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class PdfGraphExample
{
    // Deserializes a PDF from a byte array, adds a graph with text labels, and saves the result.
    public static void AddGraphWithLabels(byte[] pdfBytes, string outputPath)
    {
        // Load the PDF from the byte array using the Document(Stream) constructor.
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        using (Document doc = new Document(ms))
        {
            // Ensure there is at least one page.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Create a Graph container (width: 400, height: 200) – use double overload as required.
            Graph graph = new Graph(400.0, 200.0);

            // ----- Add a rectangle shape -----
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rectShape);

            // ----- Add a line shape -----
            float[] linePoints = { 300f, 200f, 350f, 250f };
            Line lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(lineShape);

            // ----- Add an ellipse shape -----
            Ellipse ellipseShape = new Ellipse(100f, 50f, 150f, 80f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Blue,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // ----- Add text labels for each shape -----
            TextFragment CreateLabel(string text, double x, double y)
            {
                TextFragment tf = new TextFragment(text);
                tf.Position = new Position(x, y);
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Black;
                return tf;
            }

            // Create a TextBuilder for the page.
            TextBuilder builder = new TextBuilder(page);

            // Label for rectangle.
            builder.AppendText(CreateLabel("Aspose.Pdf.Rectangle", 60, 260));

            // Label for line.
            builder.AppendText(CreateLabel("Line", 310, 210));

            // Label for ellipse.
            builder.AppendText(CreateLabel("Ellipse", 110, 130));

            // Save the modified PDF.
            doc.Save(outputPath);
        }
    }

    // Example usage.
    static void Main()
    {
        // -------------------------------------------------------------------
        // Create a minimal PDF in memory so we do not depend on an external file.
        // This satisfies the "deserialize from byte[]" requirement while keeping the
        // example self‑contained and free of FileNotFoundException.
        // -------------------------------------------------------------------
        byte[] pdfBytes;
        using (MemoryStream tempStream = new MemoryStream())
        {
            // Create a simple one‑page PDF.
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(tempStream);
            }
            pdfBytes = tempStream.ToArray();
        }

        string outputPath = "output_with_graph.pdf";
        AddGraphWithLabels(pdfBytes, outputPath);
        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
