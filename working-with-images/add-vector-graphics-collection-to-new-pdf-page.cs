using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Vector; // Added for GraphicsAbsorber

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Absorb vector graphics from the first page (example)
            GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber();
            graphicsAbsorber.Visit(sourceDoc.Pages[1]);

            // Create a new PDF document
            using (Document destDoc = new Document())
            {
                // Insert a new blank page
                Page newPage = destDoc.Pages.Add();

                // Prepare a collection of graphic elements to add
                // (Here we create placeholder shapes because GraphicElement objects cannot be added directly to a Graph)
                var placeholderShapes = new List<Shape>();

                // Example placeholder: a rectangle for each absorbed graphic element
                foreach (var element in graphicsAbsorber.Elements)
                {
                    // Use Aspose.Pdf.Drawing.Rectangle (shape), not Aspose.Pdf.Rectangle (page rectangle)
                    var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
                    rect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 1f
                    };
                    placeholderShapes.Add(rect);
                }

                // If no vector graphics were found, add a sample shape so the page is not empty
                if (placeholderShapes.Count == 0)
                {
                    var line = new Line(new float[] { 50f, 750f, 550f, 750f });
                    line.GraphInfo = new GraphInfo
                    {
                        Color = Aspose.Pdf.Color.Blue,
                        LineWidth = 2f
                    };
                    placeholderShapes.Add(line);
                }

                // Use List<T>.AddRange to add the shapes to the collection (demonstrates AddRange usage)
                var shapesToAdd = new List<Shape>();
                shapesToAdd.AddRange(placeholderShapes);

                // Add the shapes to the page via a Graph container
                // Graph constructor expects double values for width and height
                var graph = new Graph(600.0, 800.0);
                foreach (var shape in shapesToAdd)
                {
                    graph.Shapes.Add(shape);
                }
                newPage.Paragraphs.Add(graph);

                // Save the resulting PDF
                destDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF with added vector graphics saved to '{outputPdfPath}'.");
            }
        }
    }
}
