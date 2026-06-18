using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class PdfGenerator
{
    // Dummy entry point to satisfy the compiler when building as an executable.
    public static void Main(string[] args)
    {
        // No operation – the library method can be called from other code.
        // Example usage (optional):
        // byte[] pdfBytes = CreatePdfWithGraph();
    }

    public static byte[] CreatePdfWithGraph()
    {
        // MemoryStream will hold the generated PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Create a new PDF document (wrapped in using for proper disposal)
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // Create a Graph container (width, height in points) – use double constructor as required
                Graph graph = new Graph(400.0, 200.0);

                // Create a rectangle shape inside the graph – use float parameters
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f // float literal
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);

                // Save the PDF into the memory stream
                doc.Save(outputStream);
            }

            // Return the PDF as a byte array
            return outputStream.ToArray();
        }
    }
}