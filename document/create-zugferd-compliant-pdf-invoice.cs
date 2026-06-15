using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "invoice.xml";          // ZUGFeRD XML invoice data
        const string outputPdf = "invoice_zugferd.pdf"; // Resulting PDF file
        const string logPath   = "conversion.log";      // Conversion log

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Create a new empty PDF document
            using (Document doc = new Document())
            {
                // Embed the ZUGFeRD XML into the PDF
                doc.BindXml(xmlPath);

                // Convert the document to ZUGFeRD format, logging any conversion errors
                doc.Convert(logPath, PdfFormat.ZUGFeRD, ConvertErrorAction.Delete);

                // Save the ZUGFeRD‑compliant PDF
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