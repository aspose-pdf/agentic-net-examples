using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PreserveCDataToPdf
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document (no XSL transformation needed)
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page (you can add more pages as needed)
            Page page = pdfDoc.Pages.Add();

            // Starting coordinates for the first CDATA block
            double cursorY = page.PageInfo.Height - 50; // top margin
            const double marginX = 50;
            const double lineSpacing = 20;

            // Recursively process all CDATA sections in the XML
            ProcessNode(xmlDoc, ref cursorY, marginX, lineSpacing, page);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created: {pdfPath}");
    }

    // Walks the XML tree and renders each CDATA section as formatted text
    private static void ProcessNode(XmlNode node, ref double cursorY, double marginX, double lineSpacing, Page page)
    {
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.CDATA)
            {
                // Create a text fragment for the CDATA content
                TextFragment tf = new TextFragment(child.Value);

                // Position the fragment on the page
                tf.Position = new Position(marginX, cursorY);

                // Apply simple formatting (you can customize as needed)
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                // Move the cursor down for the next block
                cursorY -= lineSpacing;
            }
            else
            {
                // Recurse into element nodes
                ProcessNode(child, ref cursorY, marginX, lineSpacing, page);
            }
        }
    }
}