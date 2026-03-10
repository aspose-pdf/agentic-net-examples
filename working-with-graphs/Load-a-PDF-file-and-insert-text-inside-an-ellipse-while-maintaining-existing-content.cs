using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string ellipseText = "Sample Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the ellipse and text will be added (first page in this example)
            Aspose.Pdf.Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Draw an ellipse on the page
            // -------------------------------------------------
            // Graph container size (width, height) – use double literals as required by the API
            Graph graph = new Graph(500.0, 800.0);

            // Ellipse constructor expects float values: left, bottom, width, height
            // Position the ellipse at (100, 500) with a size of 200x100
            Ellipse ellipse = new Ellipse(100.0F, 500.0F, 200.0F, 100.0F);
            ellipse.GraphInfo = new GraphInfo
            {
                // Fill with a light gray color
                FillColor = Aspose.Pdf.Color.LightGray,
                // Outline color
                Color = Aspose.Pdf.Color.Black,
                // Outline thickness
                LineWidth = 2.0F
            };
            graph.Shapes.Add(ellipse);

            // Add the graph (containing the ellipse) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // 2. Insert text inside the ellipse
            // -------------------------------------------------
            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment(ellipseText);

            // Position the text roughly at the center of the ellipse.
            // The Position property uses a Text.Position (X, Y) where Y is the baseline.
            // Adjust as needed for exact centering.
            tf.Position = new Position(200.0F, 540.0F); // X = left + width/2, Y = bottom + height/2

            // Set text appearance
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ellipse and text: '{outputPath}'.");
    }
}