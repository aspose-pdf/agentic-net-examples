using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all paragraphs on the page
                foreach (var paragraph in page.Paragraphs)
                {
                    // Graph objects are added as paragraphs; check the type
                    if (paragraph is Graph graph)
                    {
                        // Each shape in the graph is stored in the Shapes collection
                        foreach (var shape in graph.Shapes)
                        {
                            // Rectangle shape – compute its bounding box from Left/Bottom/Width/Height
                            if (shape is Aspose.Pdf.Drawing.Rectangle rectShape)
                            {
                                double llx = rectShape.Left;
                                double lly = rectShape.Bottom;
                                double urx = rectShape.Left + rectShape.Width;
                                double ury = rectShape.Bottom + rectShape.Height;
                                Console.WriteLine(
                                    $"Page {i}: Rectangle – LLX:{llx}, LLY:{lly}, URX:{urx}, URY:{ury}");
                            }
                            else if (shape is Line line)
                            {
                                // PositionArray holds the points of the line: x1, y1, x2, y2, ...
                                var pts = line.PositionArray;
                                Console.WriteLine(
                                    $"Page {i}: Line – Points:{string.Join(",", pts)}");
                            }
                            else if (shape is Curve curve)
                            {
                                var pts = curve.PositionArray;
                                Console.WriteLine(
                                    $"Page {i}: Curve – Points:{string.Join(",", pts)}");
                            }
                            else
                            {
                                // For other shape types we only log the type name
                                Console.WriteLine(
                                    $"Page {i}: Shape {shape.GetType().Name} (bounding box not directly available)");
                            }
                        }
                    }
                }
            }
        }
    }
}
