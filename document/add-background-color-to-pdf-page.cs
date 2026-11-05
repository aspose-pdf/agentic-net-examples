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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Page dimensions as floats (required by drawing APIs)
            float pageWidth = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;

            // Create a Graph that spans the whole page – the Graph is a paragraph
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create a rectangle shape that covers the page (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle bgRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, pageWidth, pageHeight);

            // Set a semi‑transparent light‑gray fill (alpha ≈ 30% → 77/255)
            bgRect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(77, 211, 211, 211);
            // Optional: no visible border
            bgRect.GraphInfo.Color = Aspose.Pdf.Color.Transparent;

            // Add the rectangle to the graph, then insert the graph at the beginning of the page content
            graph.Shapes.Add(bgRect);
            page.Paragraphs.Insert(0, graph);

            // Save the modified PDF (Document.Save writes PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color added and saved to '{outputPath}'.");
    }
}
