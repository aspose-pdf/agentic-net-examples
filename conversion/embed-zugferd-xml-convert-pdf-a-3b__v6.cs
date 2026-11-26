using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "CompliantInvoice.pdf";
        const string conversionLog = "conversion_log.xml";

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
                // Read the ZUGFeRD XML file into a memory stream
                byte[] xmlBytes = File.ReadAllBytes(xmlPath);
                using (var xmlStream = new MemoryStream(xmlBytes))
                {
                    // Create a file specification for the XML attachment
                    var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Alternative // Mark as an Alternative File (AF) entry for PDF/A‑3
                    };

                    // Add the specification to the document's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance) and write a conversion log
                doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the compliant PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3B compliant file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
