using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // JavascriptAction, LinkAnnotation, HighlightingMode
using Aspose.Pdf.Drawing;            // Graph, Line, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Load the PDF ----------
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1]; // work on the first page (adjust as needed)

        // ---------- Draw a polygon using the low‑level Graph API ----------
        // Create a Graph that covers the whole page (or at least the area we need)
        var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height)
        {
            Left = 0,
            Top = 0
        };

        // A polygon is a closed Line. The first point is repeated at the end to close the shape.
        var polygon = new Line(new float[]
        {
            100, 500,   // point 1
            200, 600,   // point 2
            300, 500,   // point 3
            200, 400,   // point 4
            100, 500    // back to point 1 to close
        })
        {
            // Styling: blue stroke, 2‑pt width, no fill (transparent)
            GraphInfo =
            {
                Color = Color.Blue,
                LineWidth = 2,
                FillColor = Color.Transparent // transparent fill = no fill
            }
        };

        // Add the polygon shape to the graph and the graph to the page.
        graph.Shapes.Add(polygon);
        page.Paragraphs.Add(graph);

        // ---------- Attach a JavaScript link using the core Document API ----------
        // Define a rectangle that covers the polygon area (llx, lly, urx, ury)
        var linkRect = new Aspose.Pdf.Rectangle(100, 400, 300, 600);
        var jsLink = new LinkAnnotation(page, linkRect)
        {
            Action = new JavascriptAction("var myVar = 42;"), // sets a variable in the PDF JavaScript context
            Highlighting = HighlightingMode.Push // optional visual feedback
        };
        page.Annotations.Add(jsLink);

        // Save the final PDF with both the polygon and the JavaScript link
        doc.Save(outputPath);
        Console.WriteLine($"PDF with polygon and JavaScript link saved to '{outputPath}'.");
    }
}
