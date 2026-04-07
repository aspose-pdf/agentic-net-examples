using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Example: create 3 pages, each containing a graph that fills the page
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page (default size will be used)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (width and height in points)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Create a Graph whose size matches the page dimensions
                Graph graph = new Graph(pageWidth, pageHeight);

                // OPTIONAL: add a simple border for visual reference.
                // The Border class may not be available in some older Aspose.PDF versions.
                // If you have a version that includes Border, you can uncomment the following lines:
                // graph.Border = new Border(graph)
                // {
                //     Width = 1,
                //     Color = Color.Black
                // };

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graphs saved to '{outputPath}'.");
    }
}
