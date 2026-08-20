using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input files
        const string xmlPath = "input.xml";
        const string xsdPath = "schema.xsd";

        // Output files
        const string pdfPath = "output.pdf";

        // Verify that input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD file not found: {xsdPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Validate XML against the XSD schema (free‑form code)
        // -------------------------------------------------
        var validationErrors = new List<string>();
        XmlReaderSettings settings = new XmlReaderSettings {
            ValidationType = ValidationType.Schema,
            DtdProcessing = DtdProcessing.Prohibit
        };
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            validationErrors.Add($"{args.Severity}: {args.Message}");
        };

        try
        {
            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { /* just iterate to trigger validation */ }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during XML validation: {ex.Message}");
            return;
        }

        if (validationErrors.Count > 0)
        {
            Console.Error.WriteLine("XML validation failed with the following errors:");
            foreach (var err in validationErrors)
                Console.Error.WriteLine(err);
            return;
        }

        Console.WriteLine("XML validation succeeded.");

        // -------------------------------------------------
        // 2. Load XML into a PDF document (using Aspose.Pdf lifecycle rules)
        // -------------------------------------------------
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions(); // no XSL transformation
        using (Document pdfDocument = new Document(xmlPath, xmlLoadOptions))
        {
            // -------------------------------------------------
            // 3. Save the generated PDF (using Aspose.Pdf lifecycle rules)
            // -------------------------------------------------
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}