using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Custom exception to mimic the removed BoundsOutOfRangeException
public class BoundsOutOfRangeException : Exception
{
    public BoundsOutOfRangeException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (A4 size: 595 x 842 points)
            Page page = doc.Pages.Add();

            // Create a Graph container that matches the page size (double overload)
            Graph graph = new Graph(595.0, 842.0);
            page.Paragraphs.Add(graph);

            // Define a rectangle shape that intentionally exceeds the page bounds
            // Constructor: left, bottom, width, height (float overload)
            double left = 500;   // near right edge
            double bottom = 800; // near top edge
            double width = 200;  // will go beyond page width
            double height = 100; // will go beyond page height

            // Use fully‑qualified Aspose.Pdf.Drawing.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)left,
                (float)bottom,
                (float)width,
                (float)height);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal as required by GraphInfo
            };

            // Manual bounds checking (Aspose.Pdf.Generator namespace and BoundsCheckMode no longer exist)
            const double pageWidth = 595;
            const double pageHeight = 842;
            double right = left + width;
            double top = bottom + height;

            try
            {
                if (right > pageWidth || top > pageHeight)
                {
                    throw new BoundsOutOfRangeException(
                        $"Shape exceeds page bounds. Right={right}, Top={top}, PageWidth={pageWidth}, PageHeight={pageHeight}.");
                }

                // Shape fits – add it to the graph
                graph.Shapes.Add(rect);
                Console.WriteLine("Shape added successfully.");
            }
            catch (BoundsOutOfRangeException ex)
            {
                // Log detailed information about the offending shape
                Console.Error.WriteLine("BoundsOutOfRangeException caught:");
                Console.Error.WriteLine($"Message: {ex.Message}");
                Console.Error.WriteLine($"Shape Type: {rect.GetType().Name}");
                Console.Error.WriteLine($"Coordinates: Left={left}, Bottom={bottom}, Width={width}, Height={height}");
                Console.Error.WriteLine($"Container Size: Width={pageWidth}, Height={pageHeight}");
            }

            // Save the document
            string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}
