using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlPath = "invoice.xml";
        const string outputPath = "invoice_with_xml.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the ZUGFeRD XML.
            // The constructor accepts the file path and an optional description.
            var fileSpec = new FileSpecification(xmlPath, "ZUGFeRD Invoice")
            {
                Description = "ZUGFeRD Invoice"
            };

            // Add the specification to the embedded files collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Verify that the XML file is present in the embedded files collection.
            bool xmlEmbedded = false;
            foreach (FileSpecification spec in doc.EmbeddedFiles)
            {
                if (string.Equals(spec.Name, Path.GetFileName(xmlPath), StringComparison.OrdinalIgnoreCase))
                {
                    xmlEmbedded = true;
                    break;
                }
            }

            Console.WriteLine(xmlEmbedded
                ? "ZUGFeRD XML successfully embedded."
                : "Failed to embed ZUGFeRD XML.");

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}