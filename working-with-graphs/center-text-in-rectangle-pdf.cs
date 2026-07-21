using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle bounds where the text will be centered
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 600; // upper‑right Y

            // Create a Graph container (size can be larger than the shape)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Create a rectangle shape (left, bottom, width, height)
            // Rectangle constructor expects float values, so cast the doubles
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                (float)llx,
                (float)lly,
                (float)(urx - llx),
                (float)(ury - lly));

            // Set visual properties of the rectangle via GraphInfo
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float literal
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph (which contains the rectangle) to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World");

            // Compute the centre of the rectangle
            double centerX = (llx + urx) / 2;
            double centerY = (lly + ury) / 2;

            // Position the text at the centre point
            tf.Position = new Position(centerX, centerY);

            // Align the text horizontally and vertically to centre it
            tf.HorizontalAlignment = HorizontalAlignment.Center;
            tf.VerticalAlignment = VerticalAlignment.Center;

            // Optional styling for the text
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}
