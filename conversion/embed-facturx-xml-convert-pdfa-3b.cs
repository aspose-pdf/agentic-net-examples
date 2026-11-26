using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "CustomDesc.pdf";

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

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Load XML data to embed
                byte[] xmlData = File.ReadAllBytes(xmlPath);
                using (var xmlStream = new MemoryStream(xmlData))
                {
                    // Create a file specification for the embedded XML
                    var fileSpec = new FileSpecification("factur-x.xml", "Invoice metadata")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Alternative,
                        // Assign the stream containing the XML content
                        Contents = xmlStream
                    };
                    // Add the specification to the document's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A‑3B (required for ZUGFeRD / Factur‑X)
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);
                doc.Convert(conversionOptions);

                // Save the resulting PDF/A‑3B file (SaveFormat.Pdf is the appropriate format after conversion)
                doc.Save(outputPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"PDF/A-3B file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
