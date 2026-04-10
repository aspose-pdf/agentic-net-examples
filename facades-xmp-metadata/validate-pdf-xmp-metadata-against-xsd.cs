using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xsdPath = "xmp_schema.xsd"; // Path to the official XMP XSD file
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

        try
        {
            // Load PDF and extract its XMP metadata using the PdfXmpMetadata facade
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(pdfPath);
                byte[] xmpData = xmp.GetXmpMetadata();

                // Validate the extracted XMP XML against the XSD schema
                bool isValid = ValidateXmlAgainstSchema(xmpData, xsdPath, logPath);

                Console.WriteLine(isValid
                    ? "XMP metadata is valid."
                    : "XMP metadata validation failed. See log for details.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Validates an XML byte array against a given XSD file.
    // Returns true if validation succeeds; otherwise false.
    static bool ValidateXmlAgainstSchema(byte[] xmlBytes, string xsdFilePath, string logFilePath)
    {
        bool valid = true;

        // Configure XML reader settings with schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema
        };
        settings.Schemas.Add(null, xsdFilePath);
        settings.ValidationEventHandler += (sender, args) =>
        {
            valid = false;
            // Append validation messages to the log file
            File.AppendAllText(logFilePath, $"{args.Severity}: {args.Message}{Environment.NewLine}");
        };

        // Parse the XML; validation occurs via the event handler
        using (MemoryStream ms = new MemoryStream(xmlBytes))
        using (XmlReader reader = XmlReader.Create(ms, settings))
        {
            while (reader.Read()) { }
        }

        return valid;
    }
}