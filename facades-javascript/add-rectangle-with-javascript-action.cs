using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing; // Graph, Rectangle (shape)

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

        try
        {
            // ------------------------------------------------------------
            // 1. Load the PDF with the high‑level Document API (required for drawing).
            // ------------------------------------------------------------
            Document doc = new Document(inputPath);
            Page page = doc.Pages[1]; // work on the first page (adjust as needed)

            // ------------------------------------------------------------
            // 2. Create a Graph container that spans the whole page.
            // ------------------------------------------------------------
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ------------------------------------------------------------
            // 3. Define the rectangle bounds (left, bottom, width, height).
            //    The same bounds will later be used for the JavaScript link.
            // ------------------------------------------------------------
            const float llx = 100f;
            const float lly = 500f;
            const float urx = 300f;
            const float ury = 550f;
            float rectWidth = urx - llx; // 200
            float rectHeight = ury - lly; // 50

            // ------------------------------------------------------------
            // 4. Create the visual rectangle shape and style it.
            // ------------------------------------------------------------
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(llx, lly, rectWidth, rectHeight);
            shapeRect.GraphInfo.Color = Aspose.Pdf.Color.Blue;          // border colour
            shapeRect.GraphInfo.LineWidth = 2;                         // border thickness
            shapeRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray; // fill colour

            // Add the shape to the graph and the graph to the page.
            graph.Shapes.Add(shapeRect);
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 5. Attach a JavaScript action to the same rectangle area.
            //    PdfContentEditor.CreateJavaScriptLink(string js, System.Drawing.Rectangle rect,
            //    int pageNumber, System.Drawing.Color borderColor) is the correct overload.
            // ------------------------------------------------------------
            var editor = new PdfContentEditor();
            editor.BindPdf(doc);
            string javaScript = "app.alert('Calculation result: ' + (5 * 7));";

            // Use System.Drawing.Rectangle for the link bounds (x, y, width, height).
            var linkRect = new System.Drawing.Rectangle(
                (int)llx,
                (int)lly,
                (int)rectWidth,
                (int)rectHeight);

            editor.CreateJavaScriptLink(javaScript, linkRect, 1, System.Drawing.Color.Transparent);

            // ------------------------------------------------------------
            // 6. Save the modified PDF.
            // ------------------------------------------------------------
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"PDF saved with rectangle and JavaScript link: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
