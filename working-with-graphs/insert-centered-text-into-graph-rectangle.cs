using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (canvas) for drawing shapes
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape (left, bottom, width, height) for the graph
            // NOTE: For Graph shapes we must use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 50f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World")
            {
                // Position the text at the centre of the rectangle (X = 100 + 200/2, Y = 50 + 100/2)
                Position = new Position(200f, 100f)
            };

            // Center the text horizontally (vertical centering is achieved by the absolute Position above)
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Optional styling
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
