using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_with_text.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Ellipse geometry (left, bottom, width, height)
            double left = 100;
            double bottom = 400;
            double width = 300;
            double height = 200;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            ellipse.GraphInfo = new GraphInfo
            {
                // Light‑blue semi‑transparent fill (alpha 128 ≈ 50% opacity)
                FillColor = Color.FromArgb(128, 204, 230, 255),
                // Dark blue border
                Color = Color.DarkBlue,
                // Thick border line
                LineWidth = 5
            };
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Create a TextFragment to be centered inside the ellipse
            TextFragment tf = new TextFragment("Hello World")
            {
                TextState =
                {
                    FontSize = 24,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Black
                }
            };

            // Compute the centre of the ellipse
            double centerX = left + width / 2;
            double centerY = bottom + height / 2;

            // Position the text fragment (Position expects lower‑left corner coordinates)
            tf.Position = new Position(centerX, centerY - tf.TextState.FontSize / 2);

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
