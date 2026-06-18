using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF to satisfy the self‑contained rule
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and add four graphs arranged in a 2×2 grid
        using (Document pdfDocument = new Document("input.pdf"))
        {
            // Page indexing is 1‑based
            Page page = pdfDocument.Pages[1];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Calculate graph dimensions (float values are required for positioning)
            double graphWidthDouble = pageWidth / 2.0 - 20.0;
            double graphHeightDouble = pageHeight / 2.0 - 20.0;
            float graphWidthFloat = (float)graphWidthDouble;
            float graphHeightFloat = (float)graphHeightDouble;

            // Graph 1 (top‑left)
            Graph graph1 = new Graph(graphWidthDouble, graphHeightDouble);
            graph1.Title = new TextFragment("Graph 1");
            graph1.Left = 10f;
            graph1.Top = 10f;

            // Graph 2 (top‑right)
            Graph graph2 = new Graph(graphWidthDouble, graphHeightDouble);
            graph2.Title = new TextFragment("Graph 2");
            graph2.Left = graphWidthFloat + 30f;
            graph2.Top = 10f;

            // Graph 3 (bottom‑left)
            Graph graph3 = new Graph(graphWidthDouble, graphHeightDouble);
            graph3.Title = new TextFragment("Graph 3");
            graph3.Left = 10f;
            graph3.Top = graphHeightFloat + 30f;

            // Graph 4 (bottom‑right)
            Graph graph4 = new Graph(graphWidthDouble, graphHeightDouble);
            graph4.Title = new TextFragment("Graph 4");
            graph4.Left = graphWidthFloat + 30f;
            graph4.Top = graphHeightFloat + 30f;

            // Add graphs to the page
            page.Paragraphs.Add(graph1);
            page.Paragraphs.Add(graph2);
            page.Paragraphs.Add(graph3);
            page.Paragraphs.Add(graph4);

            pdfDocument.Save("output.pdf");
        }
    }
}