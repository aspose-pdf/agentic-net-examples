using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // source PDF
        const string zugferdXml = "invoice.xml";       // ZUGFeRD XML file
        const string outputPdf = "invoice_with_zugferd.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(zugferdXml))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXml}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Embed the ZUGFeRD XML as an attached file (AFRelationship.Data)
                using (FileStream xmlStream = File.OpenRead(zugferdXml))
                {
                    var fileSpec = new FileSpecification(
                        xmlStream,
                        Path.GetFileName(zugferdXml),
                        "ZUGFeRD Invoice XML")
                    {
                        MIMEType = "application/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
                // The first argument is a log file path; it can be any writable location.
                doc.Convert("convert_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the updated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"ZUGFeRD XML attached and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
