using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_centered.pdf";

        // Use a using block to ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // A newly created Document is empty – add a page first
            Page page = doc.Pages.Add();

            // Use the double‑based constructor (the float overload is obsolete)
            Graph graph = new Graph(200.0, 100.0);

            // Center the graph horizontally and vertically on the page
            graph.HorizontalAlignment = HorizontalAlignment.Center;
            graph.VerticalAlignment   = VerticalAlignment.Center;

            // Optional visual styling via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1
            };

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF inside the using block
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
