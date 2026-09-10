using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define rectangle dimensions (left, bottom, width, height)
            double llx = 100;   // left X
            double lly = 500;   // lower Y
            double rectWidth = 200;
            double rectHeight = 100;

            // Create a Graph that covers the whole page (or large enough area)
            // Using page size ensures the graph can be placed anywhere on the page.
            float pageWidth = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create the rectangle shape with absolute page coordinates
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)llx,
                (float)lly,
                (float)rectWidth,
                (float)rectHeight);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // background fill
                Color = Color.Black,           // border color
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph (with the rectangle) to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World");

            // Center the text horizontally within the rectangle
            tf.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the text at the rectangle's center (baseline Y is approximated)
            tf.Position = new Position(llx + rectWidth / 2, lly + rectHeight / 2);

            // Optional styling
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the document as PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
