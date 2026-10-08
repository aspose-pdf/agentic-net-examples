using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xsdPath = "xmp_schema.xsd";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD not found: {xsdPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // Example usage of a Facades class (not required for validation)
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

            // Retrieve XMP metadata as an XML string
            string xmpXml = doc.Metadata.ToString();

            // Prepare the XML schema set
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, xsdPath);

            // Configure XML reader settings for schema validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemaSet
            };
            settings.ValidationEventHandler += ValidationEventHandler;

            // Validate the XMP metadata against the schema
            using (StringReader sr = new StringReader(xmpXml))
            using (XmlReader reader = XmlReader.Create(sr, settings))
            {
                try
                {
                    while (reader.Read()) { }
                    Console.WriteLine("XMP metadata is valid according to the schema.");
                }
                catch (XmlSchemaValidationException ex)
                {
                    Console.Error.WriteLine($"Schema validation exception: {ex.Message}");
                }
            }
        }
    }

    // Callback for validation warnings and errors
    static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
    }
}