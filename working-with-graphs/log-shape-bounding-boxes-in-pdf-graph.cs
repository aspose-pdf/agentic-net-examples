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

        // Load the PDF document (using the recommended using pattern)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Paragraphs may contain Graph objects
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Graph graph)
                    {
                        // Iterate through each Shape in the Graph
                        foreach (Shape shape in graph.Shapes)
                        {
                            // Try to obtain a bounding rectangle.
                            // Shape does not expose a direct Bounds property, so we use reflection
                            // to check if such a property exists (some Shape subclasses provide it).
                            var boundsProp = shape.GetType().GetProperty("Bounds");
                            if (boundsProp != null && boundsProp.GetValue(shape) is Aspose.Pdf.Rectangle rect)
                            {
                                Console.WriteLine(
                                    $"Page {pageIndex}: Shape {shape.GetType().Name} - BBox [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
                            }
                            else
                            {
                                // If no Bounds property, fall back to a generic message.
                                Console.WriteLine(
                                    $"Page {pageIndex}: Shape {shape.GetType().Name} - Bounding box not directly available.");
                            }
                        }
                    }
                }
            }
        }
    }
}