using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph container (width, height)
                Graph graph = new Graph(500, 400);

                // ----- Rectangle -----
                // Parameters: left, bottom, width, height
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 100);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                graph.Shapes.Add(rect);

                // ----- Ellipse -----
                // Parameters: left, bottom, width, height
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(350, 150, 150, 100);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // ----- Line -----
                // Float array: { x1, y1, x2, y2 }
                float[] linePoints = { 50, 300, 450, 300 };
                Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 3
                };
                graph.Shapes.Add(line);

                // Add the Graph to the page
                page.Paragraphs.Add(graph);

                // Configure EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    Title = "Sample EPUB with Shapes",
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB
                doc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"EPUB saved to '{outputEpub}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}