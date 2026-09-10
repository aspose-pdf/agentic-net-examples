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
        const string outputPdf   = "filled.pdf";
        const string xmlData     = "data.xml";
        const string xsdSchema   = "schema.xsd";

        if (!File.Exists(pdfTemplate) || !File.Exists(xmlData) || !File.Exists(xsdSchema))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Validate the XML file against the XSD schema.
        bool isValid = ValidateXml(xmlData, xsdSchema);
        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Import the validated XML into the PDF form.
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

    static bool ValidateXml(string xmlPath, string xsdPath)
    {
        bool valid = true;
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            Console.Error.WriteLine($"Validation error: {args.Message}");
            valid = false;
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
                valid = false;
            }
        }

        return valid;
    }
}