using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xsdPath = "ZUGFeRD1p0.xsd";

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

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Locate the embedded ZUGFeRD XML file (usually ends with .xml)
            object zugferdFile = null;
            foreach (var ef in doc.EmbeddedFiles)
            {
                var nameProp = ef.GetType().GetProperty("Name");
                if (nameProp == null) continue;
                var name = nameProp.GetValue(ef) as string;
                if (!string.IsNullOrEmpty(name) && name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    zugferdFile = ef;
                    break;
                }
            }

            if (zugferdFile == null)
            {
                Console.Error.WriteLine("No embedded ZUGFeRD XML found in the PDF.");
                return;
            }

            // Extract the XML content into a string using reflection (no direct EmbeddedFile type)
            string xmlContent;
            var getStreamMethod = zugferdFile.GetType().GetMethod("GetFileStream", Type.EmptyTypes);
            if (getStreamMethod == null)
            {
                Console.Error.WriteLine("Unable to retrieve the embedded file stream.");
                return;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream fileStream = (Stream)getStreamMethod.Invoke(zugferdFile, null))
                {
                    fileStream.CopyTo(ms);
                }
                xmlContent = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }

            // Prepare XSD schema set
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdPath);

            // Configure XML reader for validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas
            };

            bool isValid = true;
            settings.ValidationEventHandler += (sender, e) =>
            {
                isValid = false;
                Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
            };

            // Validate the XML against the schema
            using (StringReader sr = new StringReader(xmlContent))
            using (XmlReader reader = XmlReader.Create(sr, settings))
            {
                while (reader.Read()) { }
            }

            Console.WriteLine(isValid
                ? "ZUGFeRD XML is valid against the XSD."
                : "ZUGFeRD XML validation failed.");
        }
    }
}
