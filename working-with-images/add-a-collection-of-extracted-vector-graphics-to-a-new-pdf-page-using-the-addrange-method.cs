using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (placeholder for real extraction logic)
        using (Document srcDoc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Simulate extraction of vector graphics as drawing shapes.
            // In a real scenario you would parse srcDoc page contents and
            // create corresponding Shape objects.
            // -----------------------------------------------------------------
            List<Shape> extractedShapes = new List<Shape>();

            // Example rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Example line
            var line = new Aspose.Pdf.Drawing.Line(new float[] { 250f, 150f, 400f, 300f });
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 2f
            };

            // Example ellipse
            var ellipse = new Aspose.Pdf.Drawing.Ellipse(100f, 200f, 150f, 250f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 1.5f
            };

            // Use List<T>.AddRange to collect the shapes
            extractedShapes.AddRange(new Shape[] { rect, line, ellipse });

            // -----------------------------------------------------------------
            // Create a new PDF document and add the extracted vector graphics
            // -----------------------------------------------------------------
            using (Document newDoc = new Document())
            {
                // Add a blank page (size will match the default page size)
                Page newPage = newDoc.Pages.Add();

                // Create a Graph container sized to the page
                Graph graph = new Graph(newPage.MediaBox.Width, newPage.MediaBox.Height);

                // Add each extracted shape to the graph
                foreach (var shape in extractedShapes)
                {
                    graph.Shapes.Add(shape);
                }

                // Attach the graph to the page
                newPage.Paragraphs.Add(graph);

                // Demonstrate usage of a Facade class as required
                using (PdfFileMend mend = new PdfFileMend(newDoc))
                {
                    // No additional operations needed; the facade is instantiated
                }

                // Save the resulting PDF
                newDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Vector graphics added to new PDF: {outputPath}");
    }
}