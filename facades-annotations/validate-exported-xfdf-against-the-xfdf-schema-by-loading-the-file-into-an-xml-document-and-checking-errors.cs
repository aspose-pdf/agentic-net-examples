using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // List to collect validation errors
    private static readonly List<string> _validationErrors = new List<string>();

    // Validation event handler
    private static void ValidationCallback(object sender, ValidationEventArgs args)
    {
        _validationErrors.Add($"{args.Severity}: {args.Message}");
    }

    static void Main()
    {
        const string inputPdfPath   = "sample.pdf";   // PDF from which XFDF will be exported
        const string xfdfSchemaPath = "xfdf.xsd";     // Path to XFDF XSD schema file

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfSchemaPath))
        {
            Console.Error.WriteLine($"XFDF schema not found: {xfdfSchemaPath}");
            return;
        }

        // Export XFDF from the PDF using Aspose.Pdf.Facades.Form
        using (Document pdfDoc = new Document(inputPdfPath))               // Document creation (lifecycle rule)
        {
            // Initialize Form facade with the PDF document
            Form form = new Form(pdfDoc);

            // Export XFDF to a memory stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                form.ExportXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream for reading

                // Validate the exported XFDF against the schema
                bool isValid = ValidateXfdf(xfdfStream, xfdfSchemaPath);

                Console.WriteLine(isValid
                    ? "XFDF validation succeeded. No errors found."
                    : "XFDF validation failed. Errors:");

                foreach (string err in _validationErrors)
                {
                    Console.WriteLine(err);
                }
            }
        }
    }

    // Loads the XFDF XML from a stream and validates it against the provided XSD schema.
    private static bool ValidateXfdf(Stream xfdfStream, string schemaPath)
    {
        // Configure XML reader settings for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            DtdProcessing = DtdProcessing.Prohibit
        };
        settings.ValidationEventHandler += ValidationCallback;

        // Add the XFDF schema
        settings.Schemas.Add(null, schemaPath);

        // Parse and validate the XFDF document
        using (XmlReader reader = XmlReader.Create(xfdfStream, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                _validationErrors.Add($"XML parsing error: {ex.Message}");
                return false;
            }
        }

        // If no errors were collected, the document is valid
        return _validationErrors.Count == 0;
    }
}