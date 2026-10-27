using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF (can be a blank page)
        const string outputPath = "ellipse_with_text.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Use 1‑based indexing (rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define ellipse bounds: left, bottom, width, height
            // (left=100, bottom=400, width=200, height=100) – this gives a centered ellipse between (100,400) and (300,500)
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(100, 400, 200, 100);

            // Styling via GraphInfo (border thickness, color, and semi‑transparent fill)
            ellipse.GraphInfo.LineWidth = 5; // thick border
            ellipse.GraphInfo.Color = Aspose.Pdf.Color.Red; // border color
            ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 100, 150, 200); // 50% transparent fill

            // Add the ellipse to the graph, then the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Centered text inside the ellipse
            TextFragment tf = new TextFragment("Centered Text");
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Approximate center of the ellipse: ((100+300)/2, (400+500)/2) = (200, 450)
            tf.Position = new Position(200, 450);

            // Add the text fragment to the page (after the graph so it appears on top)
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse with text saved to '{outputPath}'.");
    }
}
