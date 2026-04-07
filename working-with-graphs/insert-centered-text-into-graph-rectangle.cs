using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) using the double overload
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle inside the graph
            // Parameters: left (llx), bottom (lly), width, height – all as floats
            float rectLeft   = 50f;
            float rectBottom = 50f;
            float rectWidth  = 300f;
            float rectHeight = 100f;
            // Fully‑qualified type to avoid the ambiguous reference between Aspose.Pdf.Drawing.Rectangle and Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(rectLeft, rectBottom, rectWidth, rectHeight);

            // Set visual properties of the rectangle via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Calculate the center point of the rectangle
            float centerX = rectLeft + rectWidth / 2f;
            float centerY = rectBottom + rectHeight / 2f;

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World");

            // Position the text at the rectangle's center (Position expects double values)
            tf.Position = new Position((double)centerX, (double)centerY);

            // Set text styling (font, size, color)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Center the text horizontally and vertically relative to the position
            tf.HorizontalAlignment = HorizontalAlignment.Center;
            tf.VerticalAlignment   = VerticalAlignment.Center;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with centered 'Hello World' text created successfully.");
    }
}
