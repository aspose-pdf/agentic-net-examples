using System;
using System.IO;
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

            // Define ellipse dimensions
            double ellipseLeft   = 100;   // X coordinate of left side
            double ellipseBottom = 400;   // Y coordinate of bottom side
            double ellipseWidth  = 300;   // Width of the ellipse
            double ellipseHeight = 200;   // Height of the ellipse

            // Create a Graph container (required for vector shapes)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(ellipseLeft, ellipseBottom, ellipseWidth, ellipseHeight);

            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                // Semi‑transparent fill (50% opacity)
                FillColor = Aspose.Pdf.Color.FromRgb(0.8, 0.9, 1.0), // light blue fill
                // Border (stroke) color
                Color = Aspose.Pdf.Color.DarkBlue,
                // Border thickness
                LineWidth = 5
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Create a centered TextFragment inside the ellipse
            string text = "Hello, Aspose!";
            TextFragment tf = new TextFragment(text);

            // Set text appearance
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Calculate center of the ellipse
            double centerX = ellipseLeft + ellipseWidth / 2;
            double centerY = ellipseBottom + ellipseHeight / 2;

            // Position the text so its center aligns with the ellipse center
            // TextFragment.Position uses the baseline; adjust Y to roughly center vertically
            tf.Position = new Position(centerX, centerY);

            // Center the text horizontally
            tf.HorizontalAlignment = HorizontalAlignment.Center;

            // Append the text to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF
            string outputPath = "EllipseWithText.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}