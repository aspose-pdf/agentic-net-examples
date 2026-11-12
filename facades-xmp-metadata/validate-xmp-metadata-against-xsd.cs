using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf.Facades;

class Program
{
    // Flag updated by the validation callback
    private static bool _isValid = true;

    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmpSchemaPath = "xmp_schema.xsd"; // official XMP XSD file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmpSchemaPath))
        {
            Console.Error.WriteLine($"XMP schema not found: {xmpSchemaPath}");
            return;
        }

        // Load XMP metadata from the PDF using the PdfXmpMetadata facade
        byte[] xmpBytes;
        using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
        {
            xmpFacade.BindPdf(pdfPath);
            xmpBytes = xmpFacade.GetXmpMetadata();
        }

        if (xmpBytes == null || xmpBytes.Length == 0)
        {
            Console.WriteLine("No XMP metadata present in the PDF.");
            return;
        }

        // Prepare the XML schema set with the official XMP schema
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xmpSchemaPath);

        // Configure XML reader for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += ValidationCallback;

        // Validate the XMP metadata
        using (MemoryStream ms = new MemoryStream(xmpBytes))
        using (XmlReader reader = XmlReader.Create(ms, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                _isValid = false;
            }
        }

        Console.WriteLine(_isValid ? "XMP metadata is valid." : "XMP metadata is invalid.");
    }

    // Called for each validation warning or error
    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        // Treat any warning or error as a failure
        Console.Error.WriteLine($"{e.Severity}: {e.Message}");
        _isValid = false;
    }
}
