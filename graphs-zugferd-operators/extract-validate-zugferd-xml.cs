using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";               // input PDF containing ZUGFeRD data
        const string xsdPath = "ZUGFeRD1p0.xsd";            // official ZUGFeRD XSD schema file

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
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ZUGFeRD XML is stored as an embedded file, usually named "ZUGFeRD-invoice.xml"
            FileSpecification zugferdSpec = null;
            foreach (FileSpecification fs in pdfDoc.EmbeddedFiles)
            {
                // Look for a file with .xml extension and containing "ZUGFeRD" in its name
                if (!string.IsNullOrEmpty(fs.Name) &&
                    fs.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                    fs.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    zugferdSpec = fs;
                    break;
                }
            }

            if (zugferdSpec == null)
            {
                Console.Error.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
                return;
            }

            // Extract the XML content into a memory stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                using (Stream content = zugferdSpec.Contents)
                {
                    content.CopyTo(xmlStream);
                }
                xmlStream.Position = 0; // reset for reading

                // Prepare XML schema set
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, xsdPath);

                // Configure XML reader for validation
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema,
                    Schemas = schemas,
                    DtdProcessing = DtdProcessing.Prohibit
                };
                settings.ValidationEventHandler += ValidationCallback;

                // Perform validation
                using (XmlReader reader = XmlReader.Create(xmlStream, settings))
                {
                    try
                    {
                        while (reader.Read()) { } // read through the document to trigger validation
                        Console.WriteLine("ZUGFeRD XML is valid against the provided XSD schema.");
                    }
                    catch (XmlSchemaValidationException ex)
                    {
                        Console.Error.WriteLine($"Validation error: {ex.Message}");
                    }
                }
            }
        }
    }

    // Callback to capture validation warnings and errors
    private static void ValidationCallback(object? sender, ValidationEventArgs args)
    {
        if (args.Severity == XmlSeverityType.Error)
            Console.Error.WriteLine($"Error: {args.Message}");
        else
            Console.WriteLine($"Warning: {args.Message}");
    }
}
