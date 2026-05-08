using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a Graph container and draw a rectangle inside it.
            // ------------------------------------------------------------
            // Rectangle size (width, height)
            double rectWidth = 200.0;
            double rectHeight = 100.0;

            // Position of the rectangle on the page
            double rectLeft = 100.0;
            double rectBottom = 500.0;

            // Create the Graph with double dimensions (deprecated float ctor avoided)
            Graph graph = new Graph(rectWidth, rectHeight);
            // Position the graph on the page – Graph only exposes the Left property.
            // The Bottom coordinate is implicitly 0, so we shift the graph by using a Margin.
            // To place the graph at the desired Y‑position we wrap it in a PageObject with a translation.
            graph.Left = (float)rectLeft;
            // Apply a translation to move the graph vertically to the required bottom coordinate.
            // This is done by setting the graph's Margin.Bottom property (available on Graph).
            graph.Margin = new MarginInfo { Bottom = (float)rectBottom };

            // Define the rectangle shape inside the graph (uses Drawing.Rectangle)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)rectWidth, (float)rectHeight);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray, // background of the rectangle
                Color = Color.Black,         // border color
                LineWidth = 1f               // float as required by GraphInfo
            };
            graph.Shapes.Add(rectShape);

            // Add the graph to the page. It will be placed at the coordinates set above.
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // 2. Add a TextFragment centered inside the same rectangle.
            // ------------------------------------------------------------
            // Compute the centre point of the rectangle (in page coordinates)
            double centreX = rectLeft + rectWidth / 2.0;
            double centreY = rectBottom + rectHeight / 2.0;

            TextFragment textFragment = new TextFragment("Hello World");
            textFragment.Position = new Position(centreX, centreY);
            textFragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // Optional: set font size, color, etc.
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.ForegroundColor = Color.Black;

            // Append the text fragment to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendText(textFragment);

            // ------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use using for disposal)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
