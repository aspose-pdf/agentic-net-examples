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
        if (!ValidateXml(xmlData, xsdSchema))
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
            form.Save(); // Save using Aspose.Pdf.Facades SaveableFacade method.
        }

        Console.WriteLine($"XML data successfully imported to '{outputPdf}'.");
    }

    static bool ValidateXml(string xmlPath, string xsdPath)
    {
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add(null, xsdPath);

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemaSet
        };
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { }
            }
            return true;
        }
        catch (XmlException ex)
        {
            Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            return false;
        }
    }
}