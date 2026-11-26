using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "VerifiedEmbed.pdf";
        const string logPath = "conversion_log.xml";

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
                // Embed the ZUGFeRD XML file using FileSpecification
                using (var xmlStream = new MemoryStream(File.ReadAllBytes(xmlPath)))
                {
                    var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert to PDF/A‑3B (embedded files are kept automatically)
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑3B document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3B file with embedded ZUGFeRD XML saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
