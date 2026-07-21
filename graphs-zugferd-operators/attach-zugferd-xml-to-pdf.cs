using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the ZUGFeRD XML file
        const string inputPdfPath = "invoice.pdf";
        const string zugferdXmlPath = "invoice.xml";
        const string outputPdfPath = "invoice_with_zugferd.pdf";

        // Verify that the required files exist
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

        // Load the PDF document, embed the ZUGFeRD XML, convert to PDF/A‑3B and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file with AFRelationship.Data
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                var fileSpec = new FileSpecification(
                    xmlStream,
                    Path.GetFileName(zugferdXmlPath),
                    "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance) and save
            pdfDoc.Convert(outputPdfPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
        }

        Console.WriteLine($"ZUGFeRD XML attached and saved to '{outputPdfPath}'.");
    }
}
