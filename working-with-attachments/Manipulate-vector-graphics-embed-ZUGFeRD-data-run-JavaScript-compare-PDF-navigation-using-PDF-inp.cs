using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string zugferdXmlPath    = "invoice.xml";        // ZUGFeRD XML file
        const string comparePdfPath    = "compare.pdf";        // PDF to compare navigation with
        const string outputPdfPath     = "output.pdf";         // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }
        if (!File.Exists(comparePdfPath))
        {
            Console.Error.WriteLine($"Comparison PDF not found: {comparePdfPath}");
            return;
        }

        // Load the main document, add graphics, embed ZUGFeRD, add JavaScript
        using (Document doc = new Document(inputPdfPath))
        {
            // ----- 1. Add vector graphics (rectangle + line) -----
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create a Graph container (width, height) – use double literals
            Graph graph = new Graph(500.0, 200.0);

            // Rectangle shape
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 150, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Line shape
            float[] linePoints = { 300, 150, 400, 250 };
            Aspose.Pdf.Drawing.Line lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ----- 2. Embed ZUGFeRD XML data -----
            // BindXml attaches the XML file to the PDF (used for ZUGFeRD)
            doc.BindXml(zugferdXmlPath);

            // ----- 3. Add JavaScript that runs on document open -----
            // OpenAction is read‑only for setting; assign a JavascriptAction instance
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // ----- 4. Compare navigation (link annotations) with another PDF -----
            // Helper method to count link annotations in a document
            int CountLinks(string pdfPath)
            {
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                {
                    annotEditor.BindPdf(pdfPath);
                    // Extract link annotations from all pages (1 to page count)
                    AnnotationType[] types = new AnnotationType[] { AnnotationType.Link };
                    var links = annotEditor.ExtractAnnotations(1, annotEditor.Document.Pages.Count, types);
                    return links?.Count ?? 0;
                }
            }

            int mainDocLinkCount = CountLinks(inputPdfPath);
            int compareDocLinkCount = CountLinks(comparePdfPath);

            Console.WriteLine($"Link annotations – main document: {mainDocLinkCount}");
            Console.WriteLine($"Link annotations – compare document: {compareDocLinkCount}");
            if (mainDocLinkCount == compareDocLinkCount)
                Console.WriteLine("Navigation link count matches.");
            else
                Console.WriteLine("Navigation link count differs.");

            // ----- 5. Save the modified PDF -----
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}