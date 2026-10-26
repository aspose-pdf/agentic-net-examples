using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";
        const string xsdPath      = "schema.xsd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD schema file not found: {xsdPath}");
            return;
        }

        // Validate XML against the XSD schema
        bool isValid = true;
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation error: {e.Message}");
            isValid = false;
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            // Reading the entire document triggers validation
            while (reader.Read()) { }
        }

        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. PDF generation aborted.");
            return;
        }

        // XML is valid – proceed to generate a PDF
        using (Document pdfDoc = new Document())
        {
            // Add a new page
            Page page = pdfDoc.Pages.Add();

            // Add a simple text fragment
            TextFragment fragment = new TextFragment("PDF generated after successful XML validation.");
            page.Paragraphs.Add(fragment);

            // Save the PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
    }
}