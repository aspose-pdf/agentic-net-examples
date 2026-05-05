using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;          // Aspose.Pdf.Facades is required by the task
using Aspose.Pdf;                  // Core API for PDF handling (used indirectly by Form)

class XfdfValidator
{
    // Path to the PDF from which XFDF will be exported
    private const string PdfPath = "input.pdf";

    // Path where the exported XFDF will be saved
    private const string XfdfPath = "exported.xfdf";

    // Path to the XFDF schema (XSD) file
    private const string XfdfSchemaPath = "xfdf.xsd";

    static void Main()
    {
        try
        {
            // ------------------------------------------------------------
            // 1. Export annotations (or form fields) from the PDF to XFDF
            // ------------------------------------------------------------
            // Form is a Facade class that works with AcroForm data.
            // It can export the content of the fields (or annotations) to XFDF.
            using (Form form = new Form(PdfPath))
            using (FileStream xfdfStream = new FileStream(XfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
                // No explicit Save() is needed; ExportXfdf writes directly to the stream.
            }

            // ------------------------------------------------------------
            // 2. Validate the exported XFDF against the XFDF XSD schema
            // ------------------------------------------------------------
            List<string> validationErrors = new List<string>();

            // Configure XML reader settings for schema validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                DtdProcessing = DtdProcessing.Prohibit
            };
            settings.Schemas.Add(null, XfdfSchemaPath);
            settings.ValidationEventHandler += (sender, args) =>
            {
                // Collect all validation errors (warnings are also captured)
                validationErrors.Add($"{args.Severity}: {args.Message}");
            };

            // Parse the XFDF file with the configured settings
            using (FileStream xfdfFile = new FileStream(XfdfPath, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(xfdfFile, settings))
            {
                // Load the entire document; any schema violations will trigger the event handler
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
            }

            // ------------------------------------------------------------
            // 3. Report validation results
            // ------------------------------------------------------------
            if (validationErrors.Count == 0)
            {
                Console.WriteLine("XFDF validation succeeded. No errors found.");
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
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}