using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
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

        // Load the XML directly into a PDF document.
        // XmlLoadOptions without an XSL stylesheet performs a straightforward conversion.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Parse the XML separately to locate CDATA sections.
            // CDATA content appears as text nodes in the DOM.
            XmlDocument xmlDoc = new XmlDocument
            {
                PreserveWhitespace = true // keep original whitespace
            };
            xmlDoc.Load(xmlPath);

            // Use the first page as the starting point.
            Page page = pdfDoc.Pages[1];
            // Starting Y coordinate (top margin).
            double y = page.PageInfo.Height - 50;

            // Iterate over all text nodes, which include CDATA content.
            foreach (XmlNode node in xmlDoc.SelectNodes("//text()"))
            {
                string cdataText = node.Value;
                if (string.IsNullOrEmpty(cdataText))
                    continue;

                // Create a TextFragment that displays the CDATA exactly as it appears.
                TextFragment fragment = new TextFragment(cdataText);
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the fragment on the page.
                fragment.Position = new Position(50, y);
                page.Paragraphs.Add(fragment);

                // Move the Y coordinate down for the next fragment.
                y -= fragment.TextState.FontSize + 5;

                // If we run out of space, add a new page.
                if (y < 50)
                {
                    page = pdfDoc.Pages.Add();
                    y = page.PageInfo.Height - 50;
                }
            }

            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created: {pdfPath}");
    }
}