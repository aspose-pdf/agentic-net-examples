using System;
using System.Collections;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "Invoice.pdf";
        const string xmlPath   = "factur-x.xml";
        const string outputPdf = "InvoiceAttachment.pdf";
        const string logPath   = "conversion_log.xml";

        // Verify input files exist
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
            // Load the source PDF
            using (Document doc = new Document(pdfPath))
            {
                // Create a FileSpecification for the ZUGFeRD XML
                var fileSpec = new FileSpecification(xmlPath, "ZUGFeRD invoice data")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Data // ZUGFeRD requires Data relationship
                };

                // Add the specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert to PDF/A-3B (PDF/A level 3, conformance B)
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A-3B document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A-3B file with embedded ZUGFeRD XML saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
