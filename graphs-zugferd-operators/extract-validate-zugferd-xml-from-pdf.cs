using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";          // PDF containing ZUGFeRD XML
        const string zugFerdXsdPath = "ZUGFeRD-invoice.xsd"; // Official ZUGFeRD XSD schema
        const string extractedXmlPath = "extracted.xml";    // Where to save the extracted XML

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(zugFerdXsdPath))
        {
            Console.Error.WriteLine($"XSD schema not found: {zugFerdXsdPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // The ZUGFeRD XML is stored as an embedded file (usually with .xml extension)
            // Iterate over all embedded files and pick the first XML file using reflection
            bool xmlFound = false;
            foreach (var embedded in pdfDocument.EmbeddedFiles)
            {
                // Use reflection to read the "Name" property
                var nameProp = embedded.GetType().GetProperty("Name");
                if (nameProp == null) continue;
                var name = nameProp.GetValue(embedded) as string;
                if (string.IsNullOrEmpty(name)) continue;

                if (name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Extract the embedded XML into a memory stream via reflection
                    using (MemoryStream xmlStream = new MemoryStream())
                    {
                        // The Save method that accepts a Stream is present on the embedded file object
                        var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(Stream) });
                        if (saveMethod != null)
                        {
                            saveMethod.Invoke(embedded, new object[] { xmlStream });
                        }
                        else
                        {
                            // Fallback: try the overload that accepts a file path
                            var savePathMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                            if (savePathMethod != null)
                            {
                                savePathMethod.Invoke(embedded, new object[] { extractedXmlPath });
                                // Load the saved file back into the stream for validation
                                xmlStream.Write(File.ReadAllBytes(extractedXmlPath), 0, (int)new FileInfo(extractedXmlPath).Length);
                            }
                            else
                            {
                                Console.Error.WriteLine("Unable to extract embedded XML – no suitable Save method found.");
                                return;
                            }
                        }

                        xmlStream.Position = 0;

                        // Save the extracted XML to disk (optional, for inspection)
                        File.WriteAllBytes(extractedXmlPath, xmlStream.ToArray());

                        // Prepare the XSD schema set
                        XmlSchemaSet schemaSet = new XmlSchemaSet();
                        schemaSet.Add(null, zugFerdXsdPath);

                        // Configure XML reader settings for schema validation
                        XmlReaderSettings settings = new XmlReaderSettings
                        {
                            ValidationType = ValidationType.Schema,
                            Schemas = schemaSet
                        };
                        settings.ValidationEventHandler += (sender, e) =>
                        {
                            // Report validation warnings and errors
                            Console.WriteLine($"{e.Severity}: {e.Message}");
                        };

                        // Validate the XML against the schema
                        using (XmlReader reader = XmlReader.Create(xmlStream, settings))
                        {
                            while (reader.Read()) { }
                        }

                        Console.WriteLine("ZUGFeRD XML extracted and validated successfully.");
                    }

                    xmlFound = true;
                    break; // Stop after the first matching embedded XML file
                }
            }

            if (!xmlFound)
            {
                Console.Error.WriteLine("No embedded ZUGFeRD XML file was found in the PDF.");
            }
        }
    }
}
