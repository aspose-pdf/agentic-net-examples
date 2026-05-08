using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Path to the input PDF, XMP schema (XSD) and optional log file
    const string InputPdfPath = "input.pdf";
    const string XmpSchemaPath = "xmp_schema.xsd";
    const string ValidationLogPath = "xmp_validation_log.txt";

    static void Main()
    {
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {InputPdfPath}");
            return;
        }

        if (!File.Exists(XmpSchemaPath))
        {
            Console.Error.WriteLine($"XMP schema not found: {XmpSchemaPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(InputPdfPath))
        {
            // Use PdfXmpMetadata facade to extract XMP metadata as XML bytes
            PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
            xmpFacade.BindPdf(pdfDoc);
            byte[] xmpBytes = xmpFacade.GetXmpMetadata();

            // Validate the extracted XMP against the provided XSD schema
            bool isValid = ValidateXmpAgainstSchema(xmpBytes, XmpSchemaPath, ValidationLogPath);

            Console.WriteLine(isValid
                ? "XMP metadata is valid according to the schema."
                : "XMP metadata validation failed. See log for details.");
        }
    }

    // Performs XML schema validation on the XMP byte array.
    // Returns true if validation succeeds; otherwise false.
    static bool ValidateXmpAgainstSchema(byte[] xmlData, string schemaPath, string logPath)
    {
        bool isValid = true;

        // Prepare schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, schemaPath); // No target namespace specified; adjust if needed

        // Set up XmlReaderSettings with validation enabled
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            DtdProcessing = DtdProcessing.Prohibit
        };
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Capture any validation errors or warnings
            isValid = false;
            File.AppendAllText(logPath, $"{args.Severity}: {args.Message}{Environment.NewLine}");
        };

        // Ensure previous log is cleared
        if (File.Exists(logPath))
            File.Delete(logPath);

        // Validate using an XmlReader over the XMP byte array
        using (MemoryStream ms = new MemoryStream(xmlData))
        using (XmlReader reader = XmlReader.Create(ms, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                // XML parsing errors (malformed XMP) are also treated as validation failures
                isValid = false;
                File.AppendAllText(logPath, $"XML Exception: {ex.Message}{Environment.NewLine}");
            }
        }

        return isValid;
    }
}