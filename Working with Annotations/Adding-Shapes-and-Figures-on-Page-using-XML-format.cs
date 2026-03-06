using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string xmlInput = "input.xml";          // XML source describing PDF content
        const string xslPath  = "template.xsl";      // Optional XSLT for transformation
        const string outputPdf = "output.pdf";        // Resulting PDF file
        const string outputXml = "output.xml";        // PDF saved back to XML

        if (!File.Exists(xmlInput))
        {
            Console.Error.WriteLine($"XML input not found: {xmlInput}");
            return;
        }

        // Choose XmlLoadOptions – with XSL if it exists, otherwise plain XML
        XmlLoadOptions loadOptions = File.Exists(xslPath) ? new XmlLoadOptions(xslPath) : new XmlLoadOptions();

        // Load the XML (or XML+XSL) into a PDF document
        using (Document doc = new Document(xmlInput, loadOptions))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // -------------------------------------------------
            // Add a rectangle shape using the Graph container
            // -------------------------------------------------
            Graph graph = new Graph(400, 200); // width, height of the container

            // Rectangle shape (Aspose.Pdf.Drawing.Rectangle)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 500, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Add the Graph to the page's content
            page.Paragraphs.Add(graph);

            // -------------------------------------------------
            // Add a tagged Figure element for accessibility
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Shapes");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Figure element and set its alternative text
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Rectangle shape drawn on the page";

            // Attach the figure to the structure tree
            root.AppendChild(figure);

            // -------------------------------------------------
            // Save the document as PDF and also as XML
            // -------------------------------------------------
            doc.Save(outputPdf);   // PDF output
            doc.SaveXml(outputXml); // XML representation of the PDF
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
        Console.WriteLine($"XML saved to '{outputXml}'.");
    }
}