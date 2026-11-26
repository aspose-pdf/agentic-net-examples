using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input / output paths
        const string inputPdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        string outputPdfPath = Path.Combine("output", "InvoiceFinal.pdf");
        const string conversionLogPath = "conversion_log.xml";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir))
            Directory.CreateDirectory(outputDir);

        // Load the source PDF – if it does not exist, create a minimal placeholder PDF
        Document doc;
        if (File.Exists(inputPdfPath))
        {
            doc = new Document(inputPdfPath);
        }
        else
        {
            doc = new Document();
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Placeholder invoice – source PDF not found"));
        }

        // Embed the ZUGFeRD XML file as an associated file (AF) – create the XML if missing for demo purposes
        if (!File.Exists(xmlPath))
        {
            // Minimal valid Factur‑X XML placeholder
            string placeholderXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><rsm:CrossIndustryInvoice xmlns:rsm=\"urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100\"><rsm:ExchangedDocument><ram:ID xmlns:ram=\"urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100\">INV-0001</ram:ID></rsm:ExchangedDocument></rsm:CrossIndustryInvoice>";
            File.WriteAllText(xmlPath, placeholderXml);
        }

        using (FileStream xmlStream = File.OpenRead(xmlPath))
        {
            var xmlSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD invoice data")
            {
                MIMEType = "text/xml",
                AFRelationship = AFRelationship.Data
            };
            doc.EmbeddedFiles.Add(xmlSpec);
        }

        // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
        using (FileStream logStream = new FileStream(conversionLogPath, FileMode.Create, FileAccess.Write))
        {
            doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
        }

        // Save the final PDF/A‑3B file
        doc.Save(outputPdfPath);

        Console.WriteLine($"PDF/A‑3B file saved to '{outputPdfPath}'.");
    }
}
