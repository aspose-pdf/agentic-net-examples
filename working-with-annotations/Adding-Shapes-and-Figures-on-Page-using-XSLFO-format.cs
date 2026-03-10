using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

// Alias to disambiguate the System.IO.Path class
using SysPath = System.IO.Path;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string xslFoFile = SysPath.Combine(dataDir, "input.xslfo");
        string outputPdf = SysPath.Combine(dataDir, "output.pdf");

        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoFile}");
            return;
        }

        // Load XSL-FO file into a PDF document
        Aspose.Pdf.XslFoLoadOptions loadOptions = new Aspose.Pdf.XslFoLoadOptions();
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(xslFoFile, loadOptions))
        {
            // Ensure there is at least one page
            Aspose.Pdf.Page page = pdfDoc.Pages[1];

            // Create a Graph container (width, height) – use double parameters
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(200.0, 100.0);
            // Position the graph on the page (optional)
            graph.Left = 100.0;
            graph.Top = 500.0;

            // Create a rectangle shape inside the graph
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            shapeRect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            // Add the rectangle to the graph
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // OPTIONAL: Add a FigureElement for accessibility (tagged PDF)
            ITaggedContent tagged = pdfDoc.TaggedContent;
            Aspose.Pdf.LogicalStructure.StructureElement root = tagged.RootElement;
            Aspose.Pdf.LogicalStructure.FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample rectangle figure";
            root.AppendChild(figure);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
