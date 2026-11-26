using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "Invoice.pdf";
        const string xmlPath    = "factur-x.xml";
        const string outputPath = "EmbeddedThenConverted.pdf";
        const string logPath    = "conversion.log";

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

        // Load the PDF
        using (Document doc = new Document(pdfPath))
        {
            // Embed the ZUGFeRD XML file using the EmbeddedFiles collection
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
                {
                    MIMEType      = "text/xml",
                    AFRelationship = AFRelationship.Alternative // "Alternative" relationship for PDF/A‑3
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (the conversion also makes the embedded XML ZUGFeRD‑compliant)
            doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A‑3B file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3B file with embedded ZUGFeRD XML saved to '{outputPath}'.");
    }
}
