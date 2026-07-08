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
            // NOTE: In evaluation mode Aspose.Pdf allows a maximum of 4 Graph objects.
            // Therefore we limit the number of pages to 4 to avoid the runtime
            // IndexOutOfRangeException that occurs when the limit is exceeded.
            int totalPages = 4; // changed from 5 to stay within evaluation limits

            for (int i = 1; i <= totalPages; i++)
            {
                // Add a new page (default size is A4)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (in points)
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Create a Graph that matches the page size
                Graph graph = new Graph(pageWidth, pageHeight);

                // Optional: style the graph (background fill, border color, line width)
                graph.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray, // background of the graph
                    Color     = Color.Black,    // border color
                    LineWidth = 1               // border thickness
                };

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the multi‑page PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
