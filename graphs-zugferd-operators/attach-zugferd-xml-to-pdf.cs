using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";          // Existing PDF
        const string zugferdXmlPath = "invoice.xml";        // ZUGFeRD XML file
        const string outputPdfPath = "invoice_with_zugferd.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the PDF, embed the ZUGFeRD XML, and save as PDF/A‑3B (ZUGFeRD‑compatible)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Embed the XML as an attached file with the proper AFRelationship
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(zugferdXmlPath), "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B to satisfy ZUGFeRD requirements
            pdfDoc.Convert(outputPdfPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
        }

        Console.WriteLine($"ZUGFeRD XML embedded and saved to '{outputPdfPath}'.");
    }
}
