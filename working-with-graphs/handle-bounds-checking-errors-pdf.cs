using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure we work with the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph container with double literals (recommended overload)
                Graph graph = new Graph(500.0, 800.0);

                // Enable bounds checking: throw if a shape does not fit within the page size
                // Container dimensions are the page's media box width and height
                double pageWidth  = page.MediaBox.URX - page.MediaBox.LLX;
                double pageHeight = page.MediaBox.URY - page.MediaBox.LLY;
                graph.Shapes.UpdateBoundsCheckMode(
                    BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                    pageWidth,
                    pageHeight);

                // Define a rectangle that intentionally exceeds the page bounds to trigger the exception
                // Rectangle constructor: left, bottom, width, height (all float)
                float rectLeft   = (float)(pageWidth - 50);   // near right edge
                float rectBottom = (float)(pageHeight - 50); // near top edge
                float rectWidth  = 200f; // width that will overflow
                float rectHeight = 150f; // height that will overflow
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(rectLeft, rectBottom, rectWidth, rectHeight);
                rect.GraphInfo = new GraphInfo
                {
                    Color     = Color.Red,
                    FillColor = Color.Transparent,
                    LineWidth = 2f
                };

                // Attempt to add the rectangle and handle possible bounds errors
                try
                {
                    graph.Shapes.Add(rect);
                    Console.WriteLine("Rectangle added successfully.");
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log detailed shape coordinates and container size
                    Console.Error.WriteLine("BoundsOutOfRangeException caught while adding shape:");
                    Console.Error.WriteLine($"  Shape Type : Rectangle");
                    Console.Error.WriteLine($"  Left       : {rectLeft}");
                    Console.Error.WriteLine($"  Bottom     : {rectBottom}");
                    Console.Error.WriteLine($"  Width      : {rectWidth}");
                    Console.Error.WriteLine($"  Height     : {rectHeight}");
                    Console.Error.WriteLine($"  Container Width  : {pageWidth}");
                    Console.Error.WriteLine($"  Container Height : {pageHeight}");
                    Console.Error.WriteLine($"  Message   : {ex.Message}");
                }

                // Add the graph to the page's paragraphs collection
                page.Paragraphs.Add(graph);

                // Save the modified PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
