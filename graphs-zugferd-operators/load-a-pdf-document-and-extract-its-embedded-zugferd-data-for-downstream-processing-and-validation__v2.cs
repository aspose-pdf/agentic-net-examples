using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and FileSpecification

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";          // PDF containing ZUGFeRD data
        const string outputXmlPath = "ZUGFeRD.xml";        // Extracted XML file (optional)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        string zugferdXml = null;

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all embedded files in the PDF
            foreach (FileSpecification embedded in pdfDoc.EmbeddedFiles)
            {
                // ZUGFeRD data is typically stored as an XML attachment
                if (!string.IsNullOrEmpty(embedded.Name) &&
                    embedded.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Read the XML content into a string
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // The Contents property returns a Stream with the embedded file data
                        embedded.Contents.CopyTo(ms);
                        ms.Position = 0; // Reset stream position for reading
                        using (StreamReader reader = new StreamReader(ms))
                        {
                            zugferdXml = reader.ReadToEnd();
                        }
                    }

                    // Optionally write the XML to a separate file for inspection
                    File.WriteAllText(outputXmlPath, zugferdXml);
                    Console.WriteLine($"ZUGFeRD XML extracted to '{outputXmlPath}'.");
                    break; // Assuming only one ZUGFeRD XML attachment
                }
            }
        }

        if (zugferdXml == null)
        {
            Console.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
        }
        else
        {
            // At this point 'zugferdXml' contains the raw XML string
            // It can be passed to downstream validation logic as needed
            Console.WriteLine("ZUGFeRD XML content extracted successfully.");
        }
    }
}
