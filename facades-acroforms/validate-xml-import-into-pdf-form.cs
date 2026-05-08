using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplate = "template.pdf";
        const string xmlData     = "data.xml";
        const string xsdSchema   = "schema.xsd";
        const string outputPdf   = "filled.pdf";

        if (!File.Exists(pdfTemplate) || !File.Exists(xmlData) || !File.Exists(xsdSchema))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Validate the XML against the XSD schema
        if (!ValidateXml(xmlData, xsdSchema))
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Import the validated XML into the PDF form
        using (Form form = new Form(pdfTemplate, outputPdf))
        {
            using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream);
            }
            form.Save();
        }

        Console.WriteLine($"XML data successfully imported to '{outputPdf}'.");
    }

    // Returns true if the XML conforms to the XSD schema
    static bool ValidateXml(string xmlPath, string xsdPath)
    {
        bool isValid = true;

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation error: {e.Message}");
            isValid = false;
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
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