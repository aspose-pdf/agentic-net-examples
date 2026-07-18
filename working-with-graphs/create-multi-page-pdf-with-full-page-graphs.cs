using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "MultiPageGraph.pdf";

        // Number of pages to create
        int pageCount = 3;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                // Add a new blank page (default size is A4)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (width & height in points)
                double pageWidth = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Create a Graph sized exactly to the page dimensions
                Graph graph = new Graph(pageWidth, pageHeight);

                // Set a visible border using the BorderInfo constructor (no settable Width/Color properties)
                graph.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with {pageCount} pages saved to '{outputPath}'.");
    }
}
