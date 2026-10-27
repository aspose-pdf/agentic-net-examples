using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory and obtain its byte array.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var seedDoc = new Document())
        {
            // Ensure the document has at least one page.
            seedDoc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Deserialize the PDF byte array back into a Document.
        // ------------------------------------------------------------
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var doc = new Document(inputStream))
        {
            // Ensure the document has at least one page (safety check).
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Access the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // --------------------------------------------------------
            // First graph – a simple diagonal line.
            // --------------------------------------------------------
            Graph graph = new Graph(300.0, 200.0); // width, height (double overload)
            graph.Left = 100.0; // lower‑left X coordinate
            graph.Top = 400.0;  // lower‑left Y coordinate

            Line line = new Line(new float[] { 0, 0, 300, 200 });
            line.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(line);

            page.Paragraphs.Add(graph);

            // --------------------------------------------------------
            // Second graph – bars with text labels.
            // --------------------------------------------------------
            Graph labeledGraph = new Graph(200.0, 100.0);
            labeledGraph.Left = 100.0;
            labeledGraph.Top = 650.0;

            // Bar representing "Start"
            Aspose.Pdf.Drawing.Rectangle startBar = new Aspose.Pdf.Drawing.Rectangle(20, 0, 40, 80);
            startBar.GraphInfo.FillColor = Color.SteelBlue;
            startBar.Text = new TextFragment("Start");
            labeledGraph.Shapes.Add(startBar);

            // Bar representing "End"
            Aspose.Pdf.Drawing.Rectangle endBar = new Aspose.Pdf.Drawing.Rectangle(120, 0, 140, 80);
            endBar.GraphInfo.FillColor = Color.Orange;
            endBar.Text = new TextFragment("End");
            labeledGraph.Shapes.Add(endBar);

            // Optional caption for the second graph
            labeledGraph.Title = new TextFragment("Labeled Graph");

            page.Paragraphs.Add(labeledGraph);

            // --------------------------------------------------------
            // 3. Save the modified PDF.
            // --------------------------------------------------------
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF deserialized, graphs added, and saved to 'output.pdf'.");
    }
}
