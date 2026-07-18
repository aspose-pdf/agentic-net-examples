using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePdf = "invoice_template.pdf";
        const string zugFerdXml = "invoice.xml";
        const string outputPdf   = "invoice_zugferd.pdf";
        const string logFile     = "conversion_log.xml";

        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }
        if (!File.Exists(zugFerdXml))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugFerdXml}");
            return;
        }

        try
        {
            // Load the human‑readable invoice PDF
            using (Document doc = new Document(templatePdf))
            {
                // Embed the ZUGFeRD XML data
                doc.BindXml(zugFerdXml);

                // Convert to ZUGFeRD (PDF/A‑3) format
                doc.Convert(logFile, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

                // Save the final ZUGFeRD‑compliant invoice
                doc.Save(outputPdf);
            }

            Console.WriteLine($"ZUGFeRD invoice saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}