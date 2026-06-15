using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a semi‑transparent rectangle to each page
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (double values returned by Aspose)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Graph container sized to the page (Graph ctor accepts double)
                Graph graph = new Graph(pageWidth, pageHeight);

                // Rectangle covering the whole page – constructor expects float values
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)pageWidth,
                    (float)pageHeight);

                rect.GraphInfo = new GraphInfo
                {
                    // FillColor with alpha = 128 (≈50% opacity)
                    FillColor = Color.FromArgb(128, 255, 255, 0), // semi‑transparent yellow
                    Color     = Color.Black,                     // optional stroke color
                    LineWidth = 0f                               // no border (float literal)
                };

                // Add rectangle shape to the graph
                graph.Shapes.Add(rect);

                // Insert the graph into the page content
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background color to '{outputPath}'.");
    }
}
