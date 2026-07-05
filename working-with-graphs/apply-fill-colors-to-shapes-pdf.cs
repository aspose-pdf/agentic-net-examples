using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Define a dictionary that maps shape identifiers to fill colors
        var shapeColors = new Dictionary<string, Aspose.Pdf.Color>
        {
            { "Rect1", Aspose.Pdf.Color.LightBlue },
            { "Rect2", Aspose.Pdf.Color.LightGreen },
            { "Ellipse1", Aspose.Pdf.Color.LightCoral },
            { "Line1", Aspose.Pdf.Color.DarkGray }
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as required
            Graph graph = new Graph(500.0, 400.0);

            // Position offset for shapes to avoid overlap
            double offsetX = 50;
            double offsetY = 50;
            double shapeWidth = 100;
            double shapeHeight = 60;
            int index = 0;

            foreach (var kvp in shapeColors)
            {
                // Determine shape type based on identifier prefix
                Aspose.Pdf.Drawing.Shape shape;
                if (kvp.Key.StartsWith("Rect"))
                {
                    // Aspose.Pdf.Drawing.Rectangle expects float arguments
                    shape = new Aspose.Pdf.Drawing.Rectangle(
                        (float)(offsetX + index * (shapeWidth + 10)),
                        (float)offsetY,
                        (float)shapeWidth,
                        (float)shapeHeight);
                }
                else if (kvp.Key.StartsWith("Ellipse"))
                {
                    shape = new Aspose.Pdf.Drawing.Ellipse(
                        (float)(offsetX + index * (shapeWidth + 10)),
                        (float)offsetY,
                        (float)shapeWidth,
                        (float)shapeHeight);
                }
                else // default to line for any other identifier
                {
                    float[] linePoints = {
                        (float)(offsetX + index * (shapeWidth + 10)),
                        (float)offsetY,
                        (float)(offsetX + index * (shapeWidth + 10) + shapeWidth),
                        (float)(offsetY + shapeHeight)
                    };
                    shape = new Aspose.Pdf.Drawing.Line(linePoints);
                }

                // Set the fill color (or stroke color for lines) via GraphInfo
                shape.GraphInfo = new GraphInfo
                {
                    FillColor = kvp.Value,
                    Color = kvp.Value // also set stroke color for visibility
                };

                // Add the shape to the graph
                graph.Shapes.Add(shape);
                index++;
            }

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF document (PDF is the default format)
            doc.Save("shapes_output.pdf");
        }

        Console.WriteLine("PDF with colored shapes saved as 'shapes_output.pdf'.");
    }
}
