using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing per rule: page-indexing-one-based)
            Page page = doc.Pages[1];

            // Retrieve page dimensions
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph that matches the page size (Graph constructor accepts double)
            Graph graph = new Graph(pageWidth, pageHeight);

            // OPTIONAL: add a rectangle shape that fills the page (demonstrates usage)
            // Rectangle constructor expects float values, so cast the doubles
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)pageHeight);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f // float literal as required
            };
            graph.Shapes.Add(rectShape);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF (saving inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and PDF saved to '{outputPath}'.");
    }
}
