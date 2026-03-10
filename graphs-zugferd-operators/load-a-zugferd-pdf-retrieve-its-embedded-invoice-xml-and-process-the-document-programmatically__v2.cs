using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice_zugferd.pdf";
        const string outputXmlPath = "extracted_invoice.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the ZUGFeRD PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ZUGFeRD stores the invoice XML as an embedded file (AF entry).
            // In Aspose.Pdf the embedded files are exposed via the EmbeddedFiles collection.
            // The collection uses 1‑based indexing, so we iterate safely with foreach.
            if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            string extractedXml = null;

            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                // The file name is available through the Name property.
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // The actual file data is provided by the Contents stream.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileSpec.Contents.CopyTo(ms);
                        ms.Position = 0;
                        using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            extractedXml = reader.ReadToEnd();
                        }
                    }
                    break; // stop after the first XML file is found
                }
            }

            if (string.IsNullOrEmpty(extractedXml))
            {
                Console.WriteLine("No embedded XML invoice found in the PDF.");
                return;
            }

            // Save the extracted XML to a separate file for further processing.
            File.WriteAllText(outputXmlPath, extractedXml, Encoding.UTF8);
            Console.WriteLine($"Extracted invoice XML saved to '{outputXmlPath}'.");

            // Example of further processing: load the XML into an XmlDocument.
            // var xmlDoc = new System.Xml.XmlDocument();
            // xmlDoc.Load(outputXmlPath);
            // ... process xmlDoc ...
        }
    }
}
