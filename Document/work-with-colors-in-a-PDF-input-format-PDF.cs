using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "colored_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Add a colored rectangle shape using GraphInfo
            // ------------------------------------------------------------
            // Create a Graph container (size does not affect the shape)
            Graph graph = new Graph(500.0, 300.0); // use double as required

            // Define a rectangle shape (left, bottom, width, height)
            // Use the drawing Rectangle (inherits Shape) – not Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 400, 200, 100);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray, // Fill color
                Color = Color.DarkBlue,      // Stroke color
                LineWidth = 2
            };
            graph.Shapes.Add(shapeRect);

            // Add the Graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Add a text fragment with a custom text color
            // ------------------------------------------------------------
            TextFragment tf = new TextFragment("Hello, colored text!");
            tf.TextState.ForegroundColor = Color.FromRgb(255, 0, 0); // Red text
            tf.TextState.FontSize = 14;
            page.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // 3. Add a TextAnnotation with a background color
            // ------------------------------------------------------------
            // Rectangle for the annotation (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle txtAnnRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, txtAnnRect)
            {
                Title = "Note",
                Contents = "This is a colored text annotation.",
                Color = Color.Yellow // Annotation border color (acts as background for simple notes)
            };
            page.Annotations.Add(txtAnn);

            // ------------------------------------------------------------
            // 4. Add a LinkAnnotation that points to an external URL
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(50, 700, 150, 750);
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Color.Blue // Link line color
            };
            // Use GoToURIAction for external URLs
            link.Action = new GoToURIAction("https://www.example.com");
            page.Annotations.Add(link);

            // ------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use using, save inside block)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with colors saved to '{outputPath}'.");
    }
}
