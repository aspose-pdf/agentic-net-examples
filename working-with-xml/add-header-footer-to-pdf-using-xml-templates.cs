using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string templateXmlPath = "template.xml";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(templateXmlPath))
        {
            Console.Error.WriteLine($"Template XML not found: {templateXmlPath}");
            return;
        }

        // Load header/footer definitions from the XML template
        string headerText = string.Empty;
        string footerText = string.Empty;
        try
        {
            XDocument xmlDoc = XDocument.Load(templateXmlPath);
            // Expected simple structure: <Template><Header>...</Header><Footer>...</Footer></Template>
            var root = xmlDoc.Root;
            if (root != null)
            {
                var headerElem = root.Element("Header");
                var footerElem = root.Element("Footer");
                if (headerElem != null) headerText = headerElem.Value.Trim();
                if (footerElem != null) footerText = footerElem.Value.Trim();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read template XML: {ex.Message}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply header and footer to each page using TextStamp objects
            foreach (Page page in pdfDoc.Pages)
            {
                if (!string.IsNullOrEmpty(headerText))
                {
                    TextStamp headerStamp = new TextStamp(headerText)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        // Distance from the top edge of the page (in points)
                        YIndent = 20
                    };
                    page.AddStamp(headerStamp);
                }

                if (!string.IsNullOrEmpty(footerText))
                {
                    TextStamp footerStamp = new TextStamp(footerText)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        // Distance from the bottom edge of the page (in points)
                        YIndent = 20
                    };
                    page.AddStamp(footerStamp);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with header and footer saved to '{outputPdfPath}'.");
    }
}
