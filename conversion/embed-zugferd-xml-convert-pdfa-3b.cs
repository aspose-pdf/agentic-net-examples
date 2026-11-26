using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "Order.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPdfPath = "Order_ZUGFeRD.pdf";
        const string conversionLog = "conversion_log.xml";

        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                var page = placeholder.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Placeholder invoice"));
                placeholder.Save(inputPdfPath);
            }
        }

        // Ensure the ZUGFeRD XML exists – create a minimal example if it does not.
        if (!File.Exists(xmlPath))
        {
            const string sampleXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><rsm:CrossIndustryInvoice xmlns:rsm=\"urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100\"/>";
            File.WriteAllText(xmlPath, sampleXml, Encoding.UTF8);
        }

        // Load the PDF.
        using (var doc = new Document(inputPdfPath))
        {
            // Embed the ZUGFeRD XML as an attached file.
            using (var xmlStream = new MemoryStream(File.ReadAllBytes(xmlPath)))
            {
                var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Alternative // ZUGFeRD uses the Alternative relationship
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance).
            using (var logStream = new FileStream(conversionLog, FileMode.Create, FileAccess.Write))
            {
                doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            }

            // Save the resulting PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3B with ZUGFeRD metadata saved to '{outputPdfPath}'.");
    }
}
