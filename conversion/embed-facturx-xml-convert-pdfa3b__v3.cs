using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "ZUGFeRD_Invoice.pdf";
        const string logPath = "conversion_log.xml";

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
                // Create a FileSpecification for the Factur‑X XML and add it to the EmbeddedFiles collection
                var fileSpec = new FileSpecification(xmlPath, "ZUGFeRD invoice data")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Data
                };
                // Set modification date (PDF spec uses Params.ModDate)
                fileSpec.Params.ModDate = DateTime.Now;

                doc.EmbeddedFiles.Add(fileSpec);

                // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
                using (var logStream = new FileStream(logPath, FileMode.Create, FileAccess.Write))
                {
                    doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                }

                // Save the resulting PDF/A‑3B file
                doc.Save(outputPath);
            }

            Console.WriteLine($"ZUGFeRD PDF/A‑3B saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
