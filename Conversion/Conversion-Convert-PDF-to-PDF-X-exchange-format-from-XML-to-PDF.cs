using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source XML, intermediate PDF, conversion log, and final PDF/X output
        const string xmlInputPath   = "input.xml";
        const string pdfTempPath    = "temp.pdf";
        const string pdfxOutputPath = "output_pdfx3.pdf";
        const string logPath        = "conversion_log.xml";

        // Verify the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Save the intermediate PDF (optional, can be omitted if not needed)
            doc.Save(pdfTempPath);

            // Convert the PDF to PDF/X‑3 format.
            // The conversion writes any conversion errors to the specified log file.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the resulting PDF/X‑3 document
            doc.Save(pdfxOutputPath);
        }

        Console.WriteLine($"Conversion completed. PDF/X‑3 saved to '{pdfxOutputPath}'.");
    }
}