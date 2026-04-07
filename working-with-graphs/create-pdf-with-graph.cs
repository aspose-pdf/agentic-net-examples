using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // for TextFragment

public static class PdfGenerator
{
    // Creates a PDF document in memory, adds a simple graph, and returns the PDF as a byte array.
    public static byte[] CreatePdfWithGraph()
    {
        // Use a MemoryStream to hold the PDF data.
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a blank page to the document.
                Page page = doc.Pages.Add();

                // Create a Graph container (width: 400 points, height: 200 points).
                // Use the double‑based constructor as the float overload is obsolete.
                Graph graph = new Graph(400.0, 200.0);

                // Set a title for the graph – Title expects a TextFragment, not a string.
                graph.Title = new TextFragment("Sample Graph");

                // Create a rectangle shape to draw inside the graph.
                // Aspose.Pdf.Drawing.Rectangle expects float parameters.
                var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2f // float literal
                };

                // Add the rectangle shape to the graph.
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection.
                page.Paragraphs.Add(graph);

                // Save the PDF document into the MemoryStream.
                doc.Save(outputStream);
            }

            // Return the PDF bytes.
            return outputStream.ToArray();
        }
    }

    // Dummy entry point to satisfy the compiler when the project expects a Main method.
    public static void Main()
    {
        // Example usage – generate the PDF and optionally write it to disk.
        byte[] pdfBytes = CreatePdfWithGraph();
        // System.IO.File.WriteAllBytes("sample.pdf", pdfBytes);
    }
}
