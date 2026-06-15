using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class XmpValidator
{
    // Validates XMP metadata of a PDF against an XSD schema.
    // Returns true if validation succeeds, false otherwise.
    public static bool ValidateXmp(string pdfPath, string xmpSchemaPath, string logFilePath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return false;
        }

        if (!File.Exists(xmpSchemaPath))
        {
            Console.Error.WriteLine($"XMP schema not found: {xmpSchemaPath}");
            return false;
        }

        // Capture validation messages.
        System.Text.StringBuilder messages = new System.Text.StringBuilder();

        // Load XMP metadata from the PDF using the Facades API.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] xmpData = xmp.GetXmpMetadata();

            // Prepare XML reader with schema validation.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, xmpSchemaPath);
            settings.ValidationEventHandler += (sender, args) =>
            {
                // Append each validation error or warning.
                messages.AppendLine($"{args.Severity}: {args.Message}");
            };

            // Validate the XMP XML.
            using (MemoryStream ms = new MemoryStream(xmpData))
            using (XmlReader reader = XmlReader.Create(ms, settings))
            {
                try
                {
                    while (reader.Read()) { /* reading triggers validation */ }
                }
                catch (XmlException ex)
                {
                    messages.AppendLine($"XML parsing error: {ex.Message}");
                }
            }
        }

        // Write validation log.
        File.WriteAllText(logFilePath, messages.ToString());

        // If no messages were recorded, validation succeeded.
        bool isValid = messages.Length == 0;
        Console.WriteLine(isValid ? "XMP metadata is valid." : "XMP metadata validation failed.");
        return isValid;
    }

    // Example usage.
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string schemaPath = "xmp_schema.xsd"; // Path to official XMP XSD.
        const string logPath = "validation_log.txt";

        ValidateXmp(pdfPath, schemaPath, logPath);
    }
}