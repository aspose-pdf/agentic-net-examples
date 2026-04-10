using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF containing ZUGFeRD XML
        const string xmlOutputPath = "zugferd.xml"; // extracted XML file
        const string xsdPath = "ZUGFeRD1p0.xsd";    // official ZUGFeRD XSD schema

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Locate the embedded ZUGFeRD XML file using reflection (avoids direct dependency on EmbeddedFile type)
            object zugferdFile = null;
            foreach (var ef in pdfDoc.EmbeddedFiles)
            {
                var nameProp = ef.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                if (nameProp != null)
                {
                    var name = nameProp.GetValue(ef) as string;
                    if (!string.IsNullOrEmpty(name) && name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        zugferdFile = ef;
                        break;
                    }
                }
            }

            if (zugferdFile == null)
            {
                Console.Error.WriteLine("No embedded ZUGFeRD XML found in the PDF.");
                return;
            }

            // Extract the XML to a file (lifecycle rule: use stream and Save)
            // Try to invoke Save(Stream) first; if not available, fall back to the underlying FileSpecification stream.
            bool saved = false;
            var saveMethod = zugferdFile.GetType().GetMethod("Save", new[] { typeof(Stream) });
            if (saveMethod != null)
            {
                using (FileStream xmlFile = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                {
                    saveMethod.Invoke(zugferdFile, new object[] { xmlFile });
                }
                saved = true;
            }
            else
            {
                // Fallback: access FileSpecification.Contents stream
                var fileSpecProp = zugferdFile.GetType().GetProperty("FileSpecification", BindingFlags.Public | BindingFlags.Instance);
                var fileSpec = fileSpecProp?.GetValue(zugferdFile);
                var contentsProp = fileSpec?.GetType().GetProperty("Contents", BindingFlags.Public | BindingFlags.Instance);
                var contents = contentsProp?.GetValue(fileSpec) as Stream;
                if (contents != null)
                {
                    using (FileStream outStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                    {
                        contents.CopyTo(outStream);
                    }
                    saved = true;
                }
            }

            if (!saved)
            {
                Console.Error.WriteLine("Failed to extract the embedded XML file.");
                return;
            }

            Console.WriteLine($"ZUGFeRD XML extracted to: {xmlOutputPath}");

            // Validate the extracted XML against the XSD schema
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdPath);

            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas
            };

            bool hasErrors = false;
            settings.ValidationEventHandler += (sender, e) =>
            {
                hasErrors = true;
                Console.WriteLine($"Validation {e.Severity}: {e.Message}");
            };

            using (FileStream xmlStream = new FileStream(xmlOutputPath, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(xmlStream, settings))
            {
                // Read through the document to trigger validation
                while (reader.Read()) { }
            }

            if (!hasErrors)
                Console.WriteLine("XML validation succeeded.");
            else
                Console.WriteLine("XML validation completed with errors.");
        }
    }
}
