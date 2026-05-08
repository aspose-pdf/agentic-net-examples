using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePdf = "invoice_template.pdf"; // readable invoice layout
        const string xmlData = "invoice.xml";             // ZUGFeRD XML data
        const string outputPdf = "invoice_zugferd.pdf";   // final ZUGFeRD PDF
        const string conversionLog = "conversion_log.xml"; // optional conversion log

        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }
        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data not found: {xmlData}");
            return;
        }

        try
        {
            // Load the readable PDF invoice template
            using (Document doc = new Document(templatePdf))
            {
                // Embed the ZUGFeRD XML into the PDF
                doc.BindXml(xmlData);

                // Convert the document to ZUGFeRD (PDF/A‑3) format
                doc.Convert(conversionLog, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

                // Save the ZUGFeRD‑compliant invoice
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