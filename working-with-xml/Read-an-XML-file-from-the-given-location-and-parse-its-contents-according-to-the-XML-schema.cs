using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML file and its XSD schema
        const string xmlFilePath = "input.xml";
        const string xsdFilePath = "schema.xsd";

        // Verify that both files exist
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }
        if (!File.Exists(xsdFilePath))
        {
            Console.Error.WriteLine($"XSD schema file not found: {xsdFilePath}");
            return;
        }

        // Configure XML reader settings for schema validation
        XmlReaderSettings readerSettings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        // Add the XSD schema (no namespace specified here)
        readerSettings.Schemas.Add(null, xsdFilePath);
        // Report validation errors/warnings
        readerSettings.ValidationEventHandler += (sender, e) =>
        {
            Console.WriteLine($"XML Validation {e.Severity}: {e.Message}");
        };

        // Parse and validate the XML file
        using (XmlReader xmlReader = XmlReader.Create(xmlFilePath, readerSettings))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            Console.WriteLine("XML parsed and validated successfully.");

            // Optional: bind the validated XML to a new PDF document
            using (Document pdfDoc = new Document())
            {
                // BindXml(string) binds the XML file to the PDF document
                pdfDoc.BindXml(xmlFilePath);
                // Save the resulting PDF (adjust the output path as needed)
                pdfDoc.Save("output.pdf");
                Console.WriteLine("PDF generated from XML and saved as 'output.pdf'.");
            }
        }
    }
}