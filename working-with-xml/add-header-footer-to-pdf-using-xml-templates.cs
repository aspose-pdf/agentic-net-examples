using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string headerXmlPath  = "header.xml";
        const string footerXmlPath  = "footer.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(headerXmlPath) || !File.Exists(footerXmlPath))
        {
            Console.Error.WriteLine("Header or footer XML template missing.");
            return;
        }

        // Load header text from XML
        string headerText = LoadTemplateText(headerXmlPath, "Header");
        // Load footer text from XML
        string footerText = LoadTemplateText(footerXmlPath, "Footer");

        // Process PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create reusable HeaderFooter objects
            HeaderFooter headerTemplate = new HeaderFooter();
            HeaderFooter footerTemplate = new HeaderFooter();

            // Add text fragments to the templates
            if (!string.IsNullOrEmpty(headerText))
                headerTemplate.Paragraphs.Add(new TextFragment(headerText));

            if (!string.IsNullOrEmpty(footerText))
                footerTemplate.Paragraphs.Add(new TextFragment(footerText));

            // Assign a cloned header/footer to each page
            foreach (Page page in pdfDoc.Pages)
            {
                page.Header = (HeaderFooter)headerTemplate.Clone();
                page.Footer = (HeaderFooter)footerTemplate.Clone();
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with header/footer saved to '{outputPdfPath}'.");
    }

    // Helper to extract inner text of a specific element from an XML file
    private static string LoadTemplateText(string xmlPath, string elementName)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode node = xmlDoc.SelectSingleNode($"//{elementName}");
            return node?.InnerText?.Trim() ?? string.Empty;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading '{xmlPath}': {ex.Message}");
            return string.Empty;
        }
    }
}