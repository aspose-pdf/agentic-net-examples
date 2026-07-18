using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PreserveCDataToPdf
{
    static void Main()
    {
        // Input XML file containing CDATA sections
        const string xmlPath = "input.xml";
        // Output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document using standard .NET XML APIs
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page to host the formatted text
            Page page = pdfDoc.Pages.Add();

            // Starting coordinates for the first CDATA block
            double cursorY = page.PageInfo.Height - 50; // top margin
            const double leftMargin = 50;
            const double lineSpacing = 20;

            // Iterate through all CDATA sections in the XML
            XmlNodeList cdataNodes = xmlDoc.SelectNodes("//text()"); // selects all text nodes, including CDATA
            foreach (XmlNode node in cdataNodes)
            {
                if (node.NodeType != XmlNodeType.CDATA) continue;

                // Create a TextFragment with the CDATA content
                TextFragment fragment = new TextFragment(node.Value);

                // Use a monospaced font to reflect the original formatting
                fragment.TextState.Font = FontRepository.FindFont("Courier New");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the fragment on the page
                fragment.Position = new Position(leftMargin, cursorY);
                page.Paragraphs.Add(fragment);

                // Move the cursor down for the next block
                cursorY -= lineSpacing;
                // If we run out of space, add a new page
                if (cursorY < 50)
                {
                    page = pdfDoc.Pages.Add();
                    cursorY = page.PageInfo.Height - 50;
                }
            }

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with preserved CDATA sections saved to '{pdfPath}'.");
    }
}