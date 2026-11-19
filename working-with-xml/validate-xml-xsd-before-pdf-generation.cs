using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xsdPath = "schema.xsd";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD file not found: {xsdPath}");
            return;
        }

        // Validate XML against the XSD schema
        bool isValid = true;
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, e) =>
        {
            isValid = false;
            Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { } // Force validation while reading
            }
            catch (XmlException ex)
            {
                isValid = false;
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            }
        }

        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. PDF generation aborted.");
            return;
        }

        // XML is valid – generate PDF
        using (Document pdfDoc = new Document())
        {
            // Add a page
            Page page = pdfDoc.Pages.Add();

            // Add a text fragment indicating success
            TextFragment tf = new TextFragment("XML validated successfully. PDF generated.");
            tf.TextState.FontSize = 14;
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'.");
    }
}