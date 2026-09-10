using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;
using System.Reflection;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // input PDF containing ZUGFeRD data
        const string xmlOutputPath = "zugferd.xml";    // extracted XML file
        const string xsdPath = "ZUGFeRD1p0.xsd";       // official ZUGFeRD XSD schema

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Search embedded files for the ZUGFeRD XML attachment using reflection
            object xmlEmbedded = null;
            foreach (var embedded in pdfDoc.EmbeddedFiles)
            {
                // "Name" property holds the attachment name
                PropertyInfo nameProp = embedded.GetType().GetProperty("Name");
                if (nameProp == null) continue;

                string name = nameProp.GetValue(embedded) as string;
                if (string.IsNullOrEmpty(name)) continue;

                if (name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                    name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    xmlEmbedded = embedded;
                    break;
                }
            }

            if (xmlEmbedded == null)
            {
                Console.Error.WriteLine("ZUGFeRD XML attachment not found in the PDF.");
                return;
            }

            // Try to invoke Save(Stream) – this is the most common overload
            MethodInfo saveMethod = xmlEmbedded.GetType().GetMethod("Save", new[] { typeof(Stream) });
            if (saveMethod != null)
            {
                using (FileStream fs = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                {
                    saveMethod.Invoke(xmlEmbedded, new object[] { fs });
                }
            }
            else
            {
                // Fallback to Save(string) if the Stream overload is unavailable
                MethodInfo saveStringMethod = xmlEmbedded.GetType().GetMethod("Save", new[] { typeof(string) });
                if (saveStringMethod != null)
                {
                    saveStringMethod.Invoke(xmlEmbedded, new object[] { xmlOutputPath });
                }
                else
                {
                    Console.Error.WriteLine("Unable to extract the embedded ZUGFeRD XML – no suitable Save method found.");
                    return;
                }
            }

            Console.WriteLine($"Extracted ZUGFeRD XML to '{xmlOutputPath}'.");
        }

        // Prepare XSD schema set
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        schemaSet.Add(null, xsdPath);

        // Configure XML reader for schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemaSet
        };

        bool isValid = true;
        settings.ValidationEventHandler += (sender, e) =>
        {
            Console.Error.WriteLine($"Validation {e.Severity}: {e.Message}");
            isValid = false;
        };

        // Validate the extracted XML against the ZUGFeRD XSD
        try
        {
            using (XmlReader reader = XmlReader.Create(xmlOutputPath, settings))
            {
                while (reader.Read()) { } // read through the document to trigger validation
            }
        }
        catch (XmlException ex)
        {
            Console.Error.WriteLine($"XML parsing error: {ex.Message}");
            isValid = false;
        }

        Console.WriteLine(isValid
            ? "ZUGFeRD XML is valid against the XSD schema."
            : "ZUGFeRD XML validation failed.");
    }
}
