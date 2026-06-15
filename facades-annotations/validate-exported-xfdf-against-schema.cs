using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;                     // Core API for PDF handling
using Aspose.Pdf.Facades;            // Facades API (used for PDF operations if needed)

class Program
{
    static void Main()
    {
        const string pdfPath   = "sample.pdf";      // Input PDF with annotations
        const string xfdfPath  = "exported.xfdf";   // Exported XFDF file
        const string schemaPath = "xfdf.xsd";       // XFDF schema file (must exist)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"XFDF schema not found: {schemaPath}");
            return;
        }

        // 1. Load the PDF document (using the lifecycle rule for disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // 2. Export all annotations to XFDF (core API method, no extra save options needed)
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // 3. Validate the exported XFDF against the XFDF schema
        bool isValid = ValidateXfdf(xfdfPath, schemaPath, out List<string> errors);

        // 4. Report validation result
        if (isValid)
        {
            Console.WriteLine("XFDF validation succeeded – no errors found.");
        }
        else
        {
            Console.WriteLine("XFDF validation failed. Errors:");
            foreach (string err in errors)
                Console.WriteLine($"  - {err}");
        }
    }

    /// <summary>
    /// Loads an XFDF file into an XmlDocument and validates it against the provided XSD schema.
    /// Returns true if the document conforms to the schema; otherwise false.
    /// </summary>
    static bool ValidateXfdf(string xfdfFile, string schemaFile, out List<string> validationErrors)
    {
        // Use a local list to collect errors – we cannot capture an out parameter inside a lambda.
        List<string> errors = new List<string>();

        // Prepare XML reader settings with the XFDF schema
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas.Add(null, schemaFile);
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Collect all validation errors (including warnings)
            errors.Add($"{args.Severity}: {args.Message}");
        };

        // Create an XmlReader that validates while reading the XFDF file
        using (FileStream fs = File.OpenRead(xfdfFile))
        using (XmlReader reader = XmlReader.Create(fs, settings))
        {
            try
            {
                // Parse the entire document; validation occurs via the event handler
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                // XML parsing errors (well-formedness) are also reported
                errors.Add($"XmlException: {ex.Message}");
                validationErrors = errors;
                return false;
            }
        }

        // Assign the collected errors to the out parameter
        validationErrors = errors;
        // If no errors were collected, the XFDF is valid
        return errors.Count == 0;
    }
}
