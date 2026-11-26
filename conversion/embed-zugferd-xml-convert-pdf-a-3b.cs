using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "Invoice.pdf";
        const string xmlPath    = "factur-x.xml";
        const string outputPath = "Invoice_ZUGFeRD.pdf";

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

        // Load the source PDF
        using (Document doc = new Document(pdfPath))
        {
            // Embed the ZUGFeRD XML as an associated file (AF)
            byte[] xmlData = File.ReadAllBytes(xmlPath);
            using (var xmlStream = new MemoryStream(xmlData))
            {
                var fileSpec = new FileSpecification(
                    xmlStream,
                    Path.GetFileName(xmlPath),
                    "ZUGFeRD invoice (Factur‑X) XML")
                {
                    MIMEType      = "text/xml",
                    AFRelationship = AFRelationship.Data,
                    Params = { ModDate = DateTime.UtcNow }
                };

                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3B);
            doc.Convert(conversionOptions);

            // Save the resulting PDF/A‑3B file (SaveFormat.Pdf is the correct enum for saving)
            doc.Save(outputPath, SaveFormat.Pdf);
        }

        Console.WriteLine($"PDF/A‑3B file with embedded ZUGFeRD XML saved to '{outputPath}'.");
    }
}
