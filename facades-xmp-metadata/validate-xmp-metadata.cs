using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmpSchemaPath = "xmp_schema.xsd";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpSchemaPath))
        {
            Console.Error.WriteLine($"XMP schema file not found: {xmpSchemaPath}");
            return;
        }

        // Load PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Extract XMP metadata as raw XML bytes
            PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
            xmpMetadata.BindPdf(pdfDocument);
            byte[] xmpBytes = xmpMetadata.GetXmpMetadata();

            // Validate the XMP XML against the provided XSD schema
            using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
            {
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(null, xmpSchemaPath);

                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.ValidationType = ValidationType.Schema;
                readerSettings.Schemas = schemaSet;
                readerSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                using (XmlReader xmlReader = XmlReader.Create(xmpStream, readerSettings))
                {
                    while (xmlReader.Read())
                    {
                        // Reading triggers validation events
                    }
                }
            }
        }
    }

    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        string severity = e.Severity == XmlSeverityType.Error ? "Error" : "Warning";
        Console.WriteLine($"XMP Validation {severity}: {e.Message}");
    }
}
