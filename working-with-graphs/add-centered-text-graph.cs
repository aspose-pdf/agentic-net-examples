using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class AddCenteredTextInRectangle
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF (self‑contained example)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF and add a graph with a rectangle and centered text
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a graph that covers the whole page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define rectangle dimensions (positioned at (100, 500) with size 200x100)
                float rectX = 100f;
                float rectY = 500f;
                float rectWidth = 200f;
                float rectHeight = 100f;
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
                // Set rectangle visual style via GraphInfo
                rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
                rect.GraphInfo.Color = Aspose.Pdf.Color.Black;
                rect.GraphInfo.LineWidth = 1f;
                // Add rectangle to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page
                page.Paragraphs.Add(graph);

                // Create a TextFragment with the desired text
                TextFragment tf = new TextFragment("Hello World");
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                // Center the text inside the rectangle
                tf.Position = new Position(rectX + rectWidth / 2f, rectY + rectHeight / 2f);
                tf.HorizontalAlignment = HorizontalAlignment.Center;
                tf.VerticalAlignment = VerticalAlignment.Center;

                // Append the text fragment to the page
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);

                // Save the modified document
                doc.Save("output.pdf");
            }
        }
    }
}
