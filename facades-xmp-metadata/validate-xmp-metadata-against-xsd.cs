using System;
using System.IO;
using System.Xml;
using System.Xml.Schema; // Added for XmlSchemaSet and XmlSchemaValidationFlags
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xsdPath = "xmp_schema.xsd";
        const string logPath = "validation_log.txt";

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

        // Load the XMP schema
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XML validation settings
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas
        };
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationEventHandler += (sender, e) =>
        {
            // Log each validation message
            File.AppendAllText(logPath, $"{e.Severity}: {e.Message}{Environment.NewLine}");
        };

        // Extract XMP metadata from the PDF using Aspose.Pdf.Facades
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] rawMetadata = xmp.GetXmpMetadata();

            // Validate the extracted XML against the schema
            using (MemoryStream ms = new MemoryStream(rawMetadata))
            using (XmlReader reader = XmlReader.Create(ms, settings))
            {
                try
                {
                    while (reader.Read()) { } // Triggers validation
                    Console.WriteLine("XMP validation completed. See log for details.");
                }
                catch (XmlException ex)
                {
                    File.AppendAllText(logPath, $"XML Exception: {ex.Message}{Environment.NewLine}");
                    Console.Error.WriteLine($"XML error: {ex.Message}");
                }
            }
        }
    }
}
