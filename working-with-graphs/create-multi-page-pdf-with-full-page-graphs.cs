using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Number of pages to generate
            int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default size is A4)
                Page page = doc.Pages.Add();

                // Retrieve the page rectangle to calculate width and height
                Aspose.Pdf.Rectangle pageRect = page.Rect;
                double pageWidth  = pageRect.URX - pageRect.LLX;
                double pageHeight = pageRect.URY - pageRect.LLY;

                // Create a Graph that matches the page dimensions
                Graph graph = new Graph(pageWidth, pageHeight);

                // Optional visual styling for the graph (fills the page with a light gray background)
                graph.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the document as a PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}