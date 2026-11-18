using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Determine page size
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define ellipse size
            double ellipseWidth = 200;
            double ellipseHeight = 100;

            // Calculate rectangle that bounds the ellipse (centered on the page)
            double llx = (pageWidth - ellipseWidth) / 2; // left
            double lly = (pageHeight - ellipseHeight) / 2; // bottom

            // Create a Graph container that will hold the drawing shapes
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create the ellipse shape (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(llx, lly, ellipseWidth, ellipseHeight);

            // Style the ellipse: thick black border and semi‑transparent red fill
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Color.Black,          // stroke color
                LineWidth = 3,                // thick border
                FillColor = Color.FromArgb(128, 255, 0, 0) // semi‑transparent red
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph (which now contains the ellipse) to the page
            page.Paragraphs.Add(graph);

            // Create a text fragment to be centered inside the ellipse
            TextFragment tf = new TextFragment("Hello, Ellipse!");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.White;
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the text at the centre of the ellipse
            double centerX = llx + ellipseWidth / 2;
            double centerY = lly + ellipseHeight / 2;
            tf.Position = new Position(centerX, centerY);

            // Add the text fragment to the page (after the graph so it appears on top)
            page.Paragraphs.Add(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
