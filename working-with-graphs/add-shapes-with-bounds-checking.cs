using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "graph_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF (or create a new one if empty)
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page
                Page page;
                if (doc.Pages.Count == 0)
                {
                    page = doc.Pages.Add();
                }
                else
                {
                    page = doc.Pages[1]; // 1‑based indexing
                }

                // Create a Graph that covers the whole page
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                Graph graph = new Graph((float)pageWidth, (float)pageHeight);

                // Enable bounds checking – throw if a shape does not fit the page
                graph.Shapes.UpdateBoundsCheckMode(
                    Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                    pageWidth,
                    pageHeight);

                // Helper to add a shape with exception handling
                void AddShape(Shape shape, string description)
                {
                    try
                    {
                        graph.Shapes.Add(shape);
                        Console.WriteLine($"{description} added successfully.");
                    }
                    catch (Aspose.Pdf.BoundsOutOfRangeException ex)
                    {
                        Console.WriteLine($"{description} out of bounds: {ex.Message}");
                    }
                }

                // Rectangle shape (left, bottom, width, height)
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                AddShape(rectShape, "Rectangle");

                // Ellipse shape (left, bottom, width, height)
                var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250, 0, 150, 100);
                ellipseShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                AddShape(ellipseShape, "Ellipse");

                // Line shape (array: x1, y1, x2, y2)
                var lineShape = new Aspose.Pdf.Drawing.Line(new float[] { 0, 200, 300, 200 });
                lineShape.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 2
                };
                AddShape(lineShape, "Line");

                // Add the graph (which contains the shapes) to the page
                page.Paragraphs.Add(graph);

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Graph PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}