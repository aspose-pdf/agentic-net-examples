using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define ellipse dimensions (centered on the page)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double ellipseWidth  = 300;
            double ellipseHeight = 200;
            double left   = (pageWidth  - ellipseWidth)  / 2;
            double bottom = (pageHeight - ellipseHeight) / 2;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, ellipseWidth, ellipseHeight);

            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                // Semi‑transparent fill (50% opacity) using ARGB
                FillColor = Color.FromArgb(128, 204, 230, 255), // 50% transparent light blue
                // Thick border
                Color    = Color.DarkBlue, // stroke color
                LineWidth = 5
            };

            // Add the ellipse to a Graph container and then to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Create a centered TextFragment
            TextFragment tf = new TextFragment("Centered Text");
            // Position the text at the center of the ellipse
            tf.Position = new Position(left + ellipseWidth / 2, bottom + ellipseHeight / 2);
            // Center the text horizontally (vertical centering is approximated by the position above)
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            tf.TextState.FontSize = 20;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Black;

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF
            doc.Save("EllipseWithText.pdf");
        }
    }
}
