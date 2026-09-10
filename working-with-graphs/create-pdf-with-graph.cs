using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class PdfGenerator
{
    /// <summary>
    /// Creates a PDF document in memory, adds a simple graph (rectangle shape), and returns the PDF as a byte array.
    /// </summary>
    public static byte[] CreatePdfWithGraph()
    {
        // Output stream that will hold the PDF bytes
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Document must be disposed deterministically
            using (Document doc = new Document())
            {
                // Add a new page to the document
                Page page = doc.Pages.Add();

                // Create a Graph container (width: 400pt, height: 200pt) using double values (new API)
                Graph graph = new Graph(400.0, 200.0);

                // Define a rectangle shape inside the graph using the drawing Rectangle type
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0.0f, 0.0f, 100.0f, 50.0f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1.0f
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);

                // Save the PDF into the memory stream (PDF format is implicit)
                doc.Save(outputStream);
            }

            // Return the PDF bytes
            return outputStream.ToArray();
        }
    }

    // Simple entry point for console execution (provides a Main method to satisfy the compiler)
    public static void Main()
    {
        byte[] pdfBytes = CreatePdfWithGraph();
        Console.WriteLine($"Generated PDF size: {pdfBytes.Length} bytes");
    }
}