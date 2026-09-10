using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For ConvertErrorAction enum (also in Aspose.Pdf)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdf = "invoice_template.pdf";   // Readable invoice layout
        const string zugferdXml  = "invoice.xml";           // Generated ZUGFeRD XML data
        const string outputPdf   = "invoice_zugferd.pdf";   // Resulting ZUGFeRD‑compliant PDF
        const string logFile     = "conversion_log.xml";    // Optional conversion log

        // Verify input files exist
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }
        if (!File.Exists(zugferdXml))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXml}");
            return;
        }

        try
        {
            // Load the readable invoice PDF
            using (Document doc = new Document(templatePdf))
            {
                // Embed the ZUGFeRD XML into the PDF
                doc.BindXml(zugferdXml);

                // Convert the document to PDF/A‑3 (ZUGFeRD) format.
                // The Convert method writes conversion errors to the supplied log file.
                doc.Convert(logFile, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

                // Save the final ZUGFeRD‑compliant PDF
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