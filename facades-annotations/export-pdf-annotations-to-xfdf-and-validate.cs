using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";   // source PDF with annotations
        const string xfdfPath   = "exported.xfdf"; // temporary XFDF file
        const string schemaPath = "xfdf.xsd";    // XFDF schema file (XSD)

        // Verify required files exist
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

        // ------------------------------------------------------------
        // Export annotations from the PDF to XFDF using the Form facade
        // ------------------------------------------------------------
        using (Form form = new Form(pdfPath))               // Form implements IDisposable
        {
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);               // write XFDF to file
            }
        }

        // ------------------------------------------------------------
        // Validate the exported XFDF against the XFDF XSD schema
        // ------------------------------------------------------------
        bool isValid = true;

        // Capture validation errors
        ValidationEventHandler validationHandler = (sender, e) =>
        {
            isValid = false;
            Console.WriteLine($"Validation {e.Severity}: {e.Message}");
        };

        // Configure XML reader for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, schemaPath);            // load the XFDF schema
        settings.ValidationEventHandler += validationHandler;

        // Parse the XFDF file; any schema violations trigger the handler
        using (FileStream xfdfFile = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        using (XmlReader reader = XmlReader.Create(xfdfFile, settings))
        {
            while (reader.Read()) { } // read through the entire document
        }

        // Report result
        Console.WriteLine(isValid ? "XFDF validation succeeded." : "XFDF validation failed.");
    }
}
