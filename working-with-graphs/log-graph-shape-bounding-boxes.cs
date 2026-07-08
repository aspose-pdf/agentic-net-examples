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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing as per rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all paragraphs on the page
                foreach (var paragraph in page.Paragraphs)
                {
                    // Identify Graph objects
                    if (paragraph is Graph graph)
                    {
                        Console.WriteLine($"Page {pageIndex}: Found Graph with {graph.Shapes.Count} shape(s).");

                        // Iterate through each shape in the graph
                        foreach (Shape shape in graph.Shapes)
                        {
                            // Log shape type
                            string shapeType = shape.GetType().Name;

                            // Use the container (page) size to evaluate bounds
                            bool fitsInPage = shape.CheckBounds(page.PageInfo.Width, page.PageInfo.Height);

                            // Log the result; exact bounding box coordinates are not exposed directly,
                            // but we can indicate whether the shape fits within the page bounds.
                            Console.WriteLine($"  Shape: {shapeType}, FitsInPage: {fitsInPage}");
                        }
                    }
                }
            }
        }
    }
}