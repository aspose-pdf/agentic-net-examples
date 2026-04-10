using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XfdfValidator
{
    // Entry point
    static void Main()
    {
        // Paths – replace with actual file locations
        const string inputPdfPath   = "sample.pdf";
        const string exportedXfdfPath = "sample.xfdf";
        const string xfdfSchemaPath = "xfdf.xsd"; // XFDF schema file

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

        // ------------------------------------------------------------
        // 1. Export annotations from the PDF to an XFDF file
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ExportAnnotationsToXfdf writes the XFDF file directly
            pdfDoc.ExportAnnotationsToXfdf(exportedXfdfPath);
        }

        // ------------------------------------------------------------
        // 2. Validate the exported XFDF against the XFDF schema
        // ------------------------------------------------------------
        // Collect validation errors
        List<string> validationErrors = new List<string>();

        // Configure XML reader settings for XSD validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            DtdProcessing = DtdProcessing.Prohibit
        };
        // Attach the schema – the target namespace for XFDF is typically empty,
        // so we pass null for the namespace parameter.
        settings.Schemas.Add(null, xfdfSchemaPath);
        // Event handler captures any validation issues
        settings.ValidationEventHandler += (sender, args) =>
        {
            validationErrors.Add($"{args.Severity}: {args.Message}");
        };

        // Create an XmlReader that validates while reading the XFDF file
        using (FileStream xfdfStream = File.OpenRead(exportedXfdfPath))
        using (XmlReader reader = XmlReader.Create(xfdfStream, settings))
        {
            // Load the XML document; validation occurs during parsing
            XmlDocument xfdfDoc = new XmlDocument();
            xfdfDoc.Load(reader);
        }

        // ------------------------------------------------------------
        // 3. Report validation results
        // ------------------------------------------------------------
        if (validationErrors.Count == 0)
        {
            Console.WriteLine("XFDF validation succeeded – no errors found.");
        }
        else
        {
            Console.WriteLine("XFDF validation failed with the following errors:");
            foreach (string err in validationErrors)
            {
                Console.WriteLine(err);
            }
        }
    }
}