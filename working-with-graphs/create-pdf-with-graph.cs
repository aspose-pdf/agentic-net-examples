using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class PdfGenerator
{
    // Creates a PDF in memory, adds a simple graph, and returns the PDF as a byte array.
    public static byte[] CreatePdfWithGraph()
    {
        // Output stream that will hold the generated PDF.
        using (var outputStream = new MemoryStream())
        {
            // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule).
            using (var pdfDoc = new Document())
            {
                // Add a single page to the document.
                var page = pdfDoc.Pages.Add();

                // Use the double‑parameter Graph constructor (see use-double-graph-constructor-and-float-rectangle-params rule).
                var graph = new Graph(400.0, 200.0);

                // Use Aspose.Pdf.Drawing.Rectangle for shapes (see use-drawing-rectangle-for-graph-shapes rule).
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 1f // float literal as required by GraphInfo
                };

                // Add the rectangle shape to the graph.
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection.
                page.Paragraphs.Add(graph);

                // Save the PDF into the memory stream.
                pdfDoc.Save(outputStream);
            }

            // Convert the memory stream to a byte array and return it.
            return outputStream.ToArray();
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Example usage – not required for library functionality.
        byte[] pdfBytes = PdfGenerator.CreatePdfWithGraph();
        Console.WriteLine($"Generated PDF size: {pdfBytes.Length} bytes");
    }
}
