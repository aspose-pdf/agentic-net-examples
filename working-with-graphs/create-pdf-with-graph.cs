using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class PdfGenerator
{
    /// <summary>
    /// Creates a PDF document in memory, adds a simple graph with a rectangle shape,
    /// and returns the PDF as a byte array.
    /// </summary>
    public static byte[] CreatePdfWithGraph()
    {
        // Use a using block to ensure the Document is disposed properly.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400 points, height: 200 points).
            // Use double literals as required by the constructor.
            Graph graph = new Graph(400.0, 200.0);

            // Create a rectangle shape (left: 0, bottom: 0, width: 100, height: 50).
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle).
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            // Set visual properties via GraphInfo.
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle to the graph.
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Save the document to a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                // Return the underlying byte array.
                return ms.ToArray();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building an executable.
    public static void Main()
    {
        // Optionally invoke the method to ensure it runs without errors.
        // byte[] pdfBytes = CreatePdfWithGraph();
        // Console.WriteLine($"Generated PDF size: {pdfBytes.Length} bytes");
    }
}