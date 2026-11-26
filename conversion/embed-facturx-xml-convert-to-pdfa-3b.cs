using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPdf = "CleanedInvoice.pdf";
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
                // Embed the ZUGFeRD XML as an embedded file (AFRelationship.Data)
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    var xmlSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD invoice (Factur‑X)")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(xmlSpec);
                }

                // Convert to PDF/A‑3B, automatically deleting non‑compliant elements
                using (FileStream logStream = File.Create(conversionLog))
                {
                    doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                }

                // Save the cleaned PDF/A‑3B document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Cleaned PDF/A‑3B saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
