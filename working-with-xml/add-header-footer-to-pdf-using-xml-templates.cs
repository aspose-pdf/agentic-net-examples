using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Input PDF and XML template files
        const string inputPdfPath   = "input.pdf";
        const string headerXmlPath  = "headerTemplate.xml";
        const string footerXmlPath  = "footerTemplate.xml";
        const string outputPdfPath  = "output_with_header_footer.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(headerXmlPath))
        {
            Console.Error.WriteLine($"Header template not found: {headerXmlPath}");
            return;
        }
        if (!File.Exists(footerXmlPath))
        {
            Console.Error.WriteLine($"Footer template not found: {footerXmlPath}");
            return;
        }

        // Load XML templates (assumed simple XML with a single root element containing the text)
        string headerTemplateContent = File.ReadAllText(headerXmlPath);
        string footerTemplateContent = File.ReadAllText(footerXmlPath);

        XDocument headerXml = XDocument.Parse(headerTemplateContent);
        XDocument footerXml = XDocument.Parse(footerTemplateContent);

        // Extract the raw text from the XML (you can adapt this if the XML structure is more complex)
        string headerRawText = headerXml.Root?.Value ?? string.Empty;
        string footerRawText = footerXml.Root?.Value ?? string.Empty;

        // Open the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = pdfDoc.Pages[pageIndex];

                // Replace placeholder {PageNumber} with the actual page number
                string headerText = headerRawText.Replace("{PageNumber}", pageIndex.ToString());
                string footerText = footerRawText.Replace("{PageNumber}", pageIndex.ToString());

                // Create and assign header
                Aspose.Pdf.HeaderFooter header = new Aspose.Pdf.HeaderFooter();
                Aspose.Pdf.Text.TextFragment headerFragment = new Aspose.Pdf.Text.TextFragment(headerText);
                header.Paragraphs.Add(headerFragment);
                page.Header = header;

                // Create and assign footer
                Aspose.Pdf.HeaderFooter footer = new Aspose.Pdf.HeaderFooter();
                Aspose.Pdf.Text.TextFragment footerFragment = new Aspose.Pdf.Text.TextFragment(footerText);
                footer.Paragraphs.Add(footerFragment);
                page.Footer = footer;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with headers and footers: {outputPdfPath}");
    }
}