using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "Invoice_ZUGFeRD.pdf";
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
                // Embed the ZUGFeRD XML as an associated file (AFRelationship.Data)
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    var xmlSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD invoice data")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(xmlSpec);
                }

                // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
                using (FileStream logStream = new FileStream(conversionLog, FileMode.Create, FileAccess.Write))
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
