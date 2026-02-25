using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string logPath    = "conversion_log.xml"; // log for conversion errors
        const string outputXml  = "output.xml";         // final XML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, convert it to PDF/X‑3, then save as XML.
        using (Document doc = new Document(inputPdf))
        {
            // Convert to PDF/X‑3; errors are written to the log file.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the (now PDF/X‑3) document as XML using XmlSaveOptions.
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            doc.Save(outputXml, xmlOpts);
        }

        Console.WriteLine($"Conversion completed. XML saved to '{outputXml}'.");
    }
}