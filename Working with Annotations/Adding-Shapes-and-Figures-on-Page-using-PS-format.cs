using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PostScript file (PS is supported only as an input format)
        const string inputPsPath  = "input.ps";
        // Output PDF file (PS cannot be saved as output)
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        // Load the PS file using PsLoadOptions and work with it as a regular PDF document
        using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
        {
            // Ensure there is at least one page to draw on
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // Create a Graph container (width, height) – use double literals
            Graph graph = new Graph(500.0, 800.0);

            // ----- Rectangle shape -----
            // Note: use fully qualified Aspose.Pdf.Drawing.Rectangle to avoid ambiguity
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100.0F, 600.0F, 200.0F, 100.0F);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(300.0F, 600.0F, 150.0F, 100.0F);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 1.5F
            };
            graph.Shapes.Add(ellipse);

            // ----- Line shape -----
            float[] linePoints = { 50.0F, 700.0F, 300.0F, 700.0F };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 1
            };
            graph.Shapes.Add(line);

            // Add the graph (containing all shapes) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified document as PDF (PS cannot be used as a save format)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Shapes added and document saved to '{outputPdfPath}'.");
    }
}