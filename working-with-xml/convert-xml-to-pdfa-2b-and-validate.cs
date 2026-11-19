using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlPath = "intermediate.xml";
        const string pdfAPath = "output_pdfa2b.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the original PDF and export it to XML.
        using (Document srcDoc = new Document(inputPdf))
        {
            srcDoc.Save(xmlPath, new XmlSaveOptions());
        }

        // Load the XML representation and convert it to PDF/A‑2b.
        using (Document xmlDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            xmlDoc.Convert(logPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);
            xmlDoc.Save(pdfAPath);
        }

        // Validate the resulting PDF/A‑2b file.
        using (Document pdfADoc = new Document(pdfAPath))
        {
            bool isValid = pdfADoc.Validate(logPath, PdfFormat.PDF_A_2B);
            if (isValid)
            {
                Console.WriteLine("PDF/A‑2b validation succeeded. The document is compliant.");
            }
            else
            {
                Console.WriteLine("PDF/A‑2b validation failed. See log for details.");
            }
        }
    }
}
