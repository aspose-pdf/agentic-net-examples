using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, its XSD schema and the desired PDF output.
        const string xmlPath = "input.xml";
        const string xsdPath = "schema.xsd";
        const string pdfPath = "output.pdf";

        // Ensure both files exist before proceeding.
        if (!File.Exists(xmlPath) || !File.Exists(xsdPath))
        {
            Console.Error.WriteLine("XML or XSD file not found.");
            return;
        }

        // Validate the XML against the XSD schema.
        if (!ValidateXml(xmlPath, xsdPath))
        {
            Console.Error.WriteLine("XML validation failed – PDF generation aborted.");
            return;
        }

        // Load the validated XML into a PDF document using Aspose.Pdf's XmlLoadOptions.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully generated: {pdfPath}");
    }

    // Performs XSD validation of an XML file.
    static bool ValidateXml(string xmlFile, string xsdFile)
    {
        bool isValid = true;

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        // Add the XSD schema; null namespace means use the schema's target namespace.
        settings.Schemas.Add(null, xsdFile);
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation error: {e.Message}");
            isValid = false;
        };

        // Parse the XML with the configured settings.
        using (XmlReader reader = XmlReader.Create(xmlFile, settings))
        {
            try
            {
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                return false;
            }
        }

        return isValid;
    }
}